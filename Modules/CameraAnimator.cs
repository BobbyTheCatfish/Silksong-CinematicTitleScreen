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
        public const float FadeTime = 0.5f;
        public static CameraAnimator Instance;

        public SceneInfo SceneInfo;
        private Animation[] Animations = [];
        private static readonly WaitForSeconds FadeWaiter = new(FadeTime);

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
            Animations = [.. SceneInfo.Animations];
#if DEBUG
            // Set delay and speed while debugging for faster results
            for (var i = 0; i < Animations.Length; i++)
            {
                Animations[i].StartDelay = 5;
                Animations[i].EndDelay = 5;
                Animations[i].Speed = 5;
            }
#endif

            // Shuffle animations if applicable
            if (SceneInfo.RandomOrder)
            {
                Animations.Shuffle();
            }

            StartCoroutine(ContinuousMovement());
        }

        IEnumerator ContinuousMovement()
        {
            var firstPass = true;
            while (true)
            {
                // Single animation screens should loop
                if (Animations.Length == 1)
                {
                    // Randomize starting position if applicable
                    if (!firstPass || (Animations[0].AllowReverse && Random.value > 0.5f))
                    {
                        Animations[0].Positions = Animations[0].Positions.Reverse().ToArray();
                    }
                }

                // Move through the animations
                yield return StartCoroutine(Move(firstPass));
                firstPass = false;
            }
        }

#if DEBUG
        void Update()
        {
            // Move to next position
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                StopAllCoroutines();
                index++;
                if (index == Animations.Length) index = 0;
                StartCoroutine(FadeCamera(Animations[index].Positions[0], Animations[index].UseLight));
            }
        }
#endif

        IEnumerator FadeCamera(Vector2 position, bool enableLight)
        {
            // Fade out if not already faded out
            if (MaskerBlackout._activeBlackouts.Any(b => b.lastValue != 1))
            {
                MaskerBlackout.StartMaskFade(1, FadeTime);
                HeroLightReplacement.FadeOut();

                yield return FadeWaiter;
            }

            yield return null;

            // Set next position
            Target = GameCameras.instance.tk2dCam.transform;
            Target.transform.SetPosition2D(position);

            HeroLightReplacement.SetActive(enableLight);

            // Fade back in
            yield return null;

            HeroLightReplacement.FadeIn();
            MaskerBlackout.StartMaskFade(0, FadeTime);
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

                var newX = xCurve.Evaluate(traveled);
                var newY = yCurve.Evaluate(traveled);
                var newPosition = new Vector2(newX, newY);

                Target.SetPosition2D(newPosition);

                if (velocity < 0.01f) break;

                yield return null;
            }
        }

        static Vector3[] GetPositionsRandomized(Animation animation)
        {
            var positions = animation.Positions;
            if (animation.Positions.Length > 2 && animation.AllowReverse && Random.value > 0.5f)
            {
                positions = animation.Positions.Reverse().ToArray();
            }

            return positions;
        }

        IEnumerator SingleFrame(Vector2 position, float duration, bool fade)
        {
            var elapsed = 0f;
            var frequency = 0.5f;
            var totalDuration = duration;
            if (fade) totalDuration += 0.5f;

            while (elapsed < totalDuration)
            {
                var x = (Mathf.PerlinNoise(50, Time.time * frequency) - 0.5f) * 0.05f * 2;
                var y = (Mathf.PerlinNoise(150, Time.time * frequency) - 0.5f) * 0.05f * 2;

                var newPosition = position + new Vector2(x, y);
                Target.SetPosition2D(newPosition);

                elapsed += Time.deltaTime;
                if (fade && elapsed >= duration)
                {
                    fade = false;
                    MaskerBlackout.StartMaskFade(1, FadeTime);
                    HeroLightReplacement.FadeOut();
                }
                yield return null;
            }

        }

        IEnumerator Move(bool firstPass)
        {
            for (var i = 0; i < Animations.Length; i++)
            {
                var anim = Animations[i];

                // Reverse positions if able
                var positions = GetPositionsRandomized(anim);

                // Set first position and fade (if not static animation)
                if (firstPass || Animations.Length > 1)
                {
                    yield return FadeCamera(positions[0], anim.UseLight);
                }

                // No animation, just shake in place
                if (positions.Length == 1)
                {
                    yield return StartCoroutine(SingleFrame(anim.Positions[0], anim.StartDelay + anim.EndDelay, Animations.Length > 1));
                }
                else
                {
                    // Wait for the start
                    if (anim.StartDelay > 0)
                    {
                        yield return new WaitForSeconds(anim.StartDelay);
                    }

                    // Animate through all the positions
                    yield return StartCoroutine(MoveAlongPath(positions, anim.Speed));

                    // Wait for the start
                    if (anim.EndDelay > 0)
                    {
                        yield return new WaitForSeconds(anim.EndDelay);
                    }
                }

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
