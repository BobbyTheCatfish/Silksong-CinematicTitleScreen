using Silksong.UnityHelper.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen.Modules
{
    internal class CameraAnimator : MonoBehaviour
    {
        private const float EaseTime = 1;
        public static CameraAnimator Instance;

        Animation[] Animations = [];
        public SceneInfo SceneInfo;
        static WaitForSeconds FadeWaiter = new(0.5f);

        bool GoToNext;

        Transform Target;
        int index = 0;

        void Start()
        {
            if (Instance)
            {
                Instance.StopAllCoroutines();
                DestroyImmediate(Instance.gameObject);
            }

            Instance = this;
        }

        public void Play()
        {
            StopAllCoroutines();

            Target = GameCameras.instance.tk2dCam.transform;
            //StartCoroutine(FadeCamera(Animations[0].Positions[0]));
            Animations = [.. SceneInfo.Animations];
#if DEBUG
            for (var i = 0; i < Animations.Length; i++)
            {
                Animations[i].StartDelay = 5;
                Animations[i].EndDelay = 5;
                Animations[i].Speed = 5;
            }
#endif

            if (Animations.Length == 1)
            {
                StartCoroutine(MoveSingleAnimation());
            }
            else
            {
                if (SceneInfo.RandomOrder)
                {
                    Animations.Shuffle();
                }
                StartCoroutine(ContinuousMovement());
            }
        }

        IEnumerator ContinuousMovement()
        {
            while (true)
            {
                yield return StartCoroutine(Move());
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                StopAllCoroutines();
                GoToNext = true;
                index++;
                if (index == Animations.Length) index = 0;
                StartCoroutine(FadeCamera(Animations[index].Positions[0], Animations[index].AmbientColor));
            }
        }

        IEnumerator FadeCamera(Vector2 position, Color color)
        {
            MaskerBlackout.StartMaskFade(1, 0.5f);
            yield return FadeWaiter;

            yield return null;
            Target = GameCameras.instance.tk2dCam.transform;
            Target.transform.SetPosition2D(position);

            if (color.a != 0)
            {
                GameCameras.instance.sceneColorManager.AmbientColorA = color;
                GameCameras.instance.sceneColorManager.AmbientColorB = color;
            }
            else
            {
                var managerObj = SceneManager.GetActiveScene().FindGameObject("_SceneManager");
                if (managerObj)
                {
                    var manager = managerObj.GetComponent<CustomSceneManager>();
                    GameCameras.instance.sceneColorManager.AmbientColorA = manager.defaultColor;
                    GameCameras.instance.sceneColorManager.AmbientColorB = manager.defaultColor;
                }
            }

            GameCameras.instance.sceneColorManager.UpdateScriptParameters();

            yield return null;

            MaskerBlackout.StartMaskFade(0, 0.5f);
        }

        IEnumerator MoveToNextPosition(Vector3 origin, Vector3 destination, float speed)
        {
            var distance = Vector2.Distance(origin, destination);
            if (distance <= Mathf.Epsilon) yield break;

            var velocity = Vector2.zero;

            while (true)
            {
                if (GoToNext)
                {
                    GoToNext = false;
                    break;
                }

                Vector2 newPosition;

                //Vector2.SmoothDamp(Target.position, destination, ref velocity, EaseTime, speed);
                var halfway = Vector2.Distance(Target.position, destination) > distance / 2;

                if ((destination.z == 1 && !halfway) || (origin.z == 1 && halfway))
                {
                    newPosition = Vector2.MoveTowards(Target.position, destination, speed * Time.deltaTime);
                    velocity = Vector2.one;
                }
                else
                {
                    newPosition = Vector2.SmoothDamp(Target.position, destination, ref velocity, EaseTime, speed);
                }

                Target.SetPosition2D(newPosition);

                if (velocity.magnitude < 0.01f) break;
                if ((newPosition - (Vector2)destination).magnitude < 0.01f) break;

                yield return null;
            }

            Debug.Log("Finished");
            Target.SetPosition2D(destination);
        }

        IEnumerator MoveAlongPath(Vector3[] keyframes, float speed)
        {
            var frameIndex = 0;
            var length = 0f;
            List<float> lengths = [];
            AnimationCurve xCurve = new();
            AnimationCurve yCurve = new();

            xCurve.AddKey(0, keyframes[0].x);
            yCurve.AddKey(0, keyframes[0].y);

            for (var i = 0; i < keyframes.Length - 1; i++)
            {
                var frame = keyframes[i];
                var nextFrame = keyframes[i + 1];

                var frameLength = Vector2.Distance(frame, nextFrame);
                lengths.Add(frameLength);
                length += frameLength;

                xCurve.AddKey(length, nextFrame.x);
                yCurve.AddKey(length, nextFrame.y);
            }

            var traveled = 0f;
            var velocity = 0f;

            while (traveled < length && frameIndex < keyframes.Length - 1)
            {
                traveled = Mathf.SmoothDamp(traveled, length, ref velocity, EaseTime, speed);
                //var newPosition = GetPointAlongPath(keyframes, lengths, traveled);

                var newX = xCurve.Evaluate(traveled);
                var newY = yCurve.Evaluate(traveled);
                var newPosition = new Vector2(newX, newY);

                Target.SetPosition2D(newPosition);

                if (velocity < 0.01f) break;

                yield return null;
            }
        }

        Vector2 GetPointAlongPath(Vector3[] path, List<float> lengths, float distance)
        {
            var traveled = 0f;

            for (var i = 0; i < lengths.Count; i++)
            {
                var segment = lengths[i];

                if (distance <= traveled + segment)
                {
                    var t = (distance - traveled) / segment;
                    var adjacent = path[i].x == path[i + 1].x || path[i].y == path[i + 1].y;

                    if (i == 0 || i >= path.Length - 2)
                    {
                        if (i == 0)
                        {
                            return Interpolate(path[i], path[i + 1], path[i], path[i + 2], t);
                        }
                        else
                        {
                            return Vector2.Lerp(path[i], path[i + 1], t);
                            //return Interpolate(path[i], path[i + 1], path[i - 1], path[i + 1], t);
                        }
                    }
                    return Interpolate(path[i], path[i + 1], path[i - 1], path[i + 2], t);
                }

                traveled += segment;
            }

            return path.Last();
        }


        static Vector2 Interpolate(Vector2 start, Vector2 end, Vector2 previous, Vector2 next, float t)
        {
            t = Mathf.Clamp01(t);

            var p0 = previous;
            var p1 = start;
            var p2 = end;
            var p3 = next;

            var t2 = t * t;
            var t3 = t2 * t;
            return 0.5f * (
                (2f * p1) +
                (-p0 + p2) * t +
                (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                (-p0 + 3f * p1 - 3f * p2 + p3) * t3
            );
        }

        Vector3[] GetPositions(Animation animation)
        {
            var positions = animation.Positions;
            if (animation.AllowReverse && Random.value > 0.5f)
            {
                positions = animation.Positions.Reverse().ToArray();
            }

            return positions;
        }

        IEnumerator MoveSingleAnimation()
        {
            var anim = Animations[0];

            // Reverse positions if able
            var positions = GetPositions(anim);

            // Set first position and fade
            yield return FadeCamera(positions[0], anim.AmbientColor);

            // No animation, just one lone frame
            if (positions.Length == 1)
            {
                yield break;
            }

            while (true)
            {
                // Wait for the start
                if (anim.StartDelay > 0)
                {
                    yield return new WaitForSeconds(anim.StartDelay);
                }

                // Animate through all the positions

                if (anim.Positions.Length == 2)
                {
                    for (var i = 0; i < positions.Length - 1; i++)
                    {
                        var origin = positions[i];
                        var destination = positions[i + 1];


                        yield return StartCoroutine(MoveToNextPosition(origin, destination, anim.Speed));
                    }
                }
                else
                {
                    List<List<Vector3>> groups = [];
                    List<Vector3> currentGroup = [];

                    for (var i = 0; i < positions.Length; i++)
                    {
                        var pos = positions[i];
                        currentGroup.Add(pos);
                        if (pos.z == 0 && currentGroup.Count > 1)
                        {
                            groups.Add(currentGroup);
                            currentGroup = [];
                            currentGroup.Add(pos);
                        }

                    }
                    Debug.Log(string.Join("\n", groups.Select(g => string.Join(", ", g))));

                    foreach (var group in groups)
                    { 
                        yield return StartCoroutine(MoveAlongPath(group.ToArray(), anim.Speed));
                        yield return new WaitForSeconds(anim.EndDelay);
                    }
                }

                // Reverse and repeat (so it goes back to the start)
                positions = positions.Reverse().ToArray();
            }
        }

        IEnumerator Move()
        {
            foreach (var anim in Animations)
            {
                // Reverse positions if able
                var positions = GetPositions(anim);

                // Set first position and fade
                yield return FadeCamera(positions[0], anim.AmbientColor);

                // Wait for the start
                if (anim.StartDelay > 0)
                {
                    yield return new WaitForSeconds(anim.StartDelay);
                }

                // No animation, continue to the next one
                if (positions.Length == 1)
                {
                    continue;
                }

                // Animate through all the positions
                yield return StartCoroutine(MoveAlongPath(positions, anim.Speed));
                //for (var i = 0; i < positions.Length - 1; i++)
                //{
                //    var origin = positions[i];
                //    var destination = positions[i + 1];

                //    yield return StartCoroutine(MoveToNextPosition(origin, destination, anim.Speed));

                //    // Wait for the end delay
                //    if (anim.StartDelay > 0 && destination.z != 1)
                //    {
                //        yield return new WaitForSeconds(anim.EndDelay);
                //    }
                //}

            }
        }
    }
}
