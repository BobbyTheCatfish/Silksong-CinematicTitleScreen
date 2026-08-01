using Silksong.AssetHelper.ManagedAssets;
using Silksong.UnityHelper.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen.Modules
{
    internal static class HeroLightReplacement
    {
        private static ManagedAsset<GameObject>? HeroAsset;
        private static GameObject? _LightObject;
        private static PlayMakerFSM? FSM;
        private static HeroLight? _HeroLight;

        public static GameObject? LightObject => _LightObject;
        public static HeroLight? HeroLight => _HeroLight;

        public static void FadeIn(bool instant = false)
        {
            if (!FSM || !FSM.gameObject.activeInHierarchy) return;

            if (instant) FSM.SendEvent("UP INSTANT");
            else FSM.SendEvent("UP");
        }

        public static void FadeOut(bool instant = false)
        {
            if (!FSM || !FSM.gameObject.activeInHierarchy) return;

            if (instant) FSM.SendEvent("DOWN INSTANT");
            else FSM.SendEvent("DOWN");
        }

        public static void SetActive(bool active)
        {
            if (!_LightObject) return;

            _LightObject.SetActive(active);
        }

        public static void SetHeroAsset(ManagedAsset<GameObject> asset)
        {
            HeroAsset = asset;
        }

        public static IEnumerator CreateLight()
        {
            if (_LightObject || HeroAsset == null) yield break;

            HeroAsset.Load();

            yield return HeroAsset.Handle;

            if (HeroAsset.Handle.OperationException != null)
            {
                Debug.LogError($"Error loading asset: {HeroAsset.Handle.OperationException}");
                yield break;
            }

            var hero = HeroAsset.Handle.Result;
            var lightObj = hero.FindChild("HeroLight");
            if (!lightObj) yield break;

            _LightObject = Object.Instantiate(lightObj);

            var scene = SceneManager.GetActiveScene();

            var logo = scene.FindGameObject("LogoTitle")!.transform;
            _LightObject.transform.SetParent(logo);
            _LightObject.transform.localPosition = new Vector3(0, 0, -logo.position.z);
            _LightObject.transform.SetPositionZ(0);
            _LightObject.SetActive(false);

            _LightObject.FindChild("Dust").SetActive(false);

            _HeroLight = _LightObject.AddComponent<HeroLight>();
            _HeroLight.spriteRenderer = _LightObject.GetComponent<SpriteRenderer>();
            _HeroLight.transform = _LightObject.transform;
            _HeroLight.vignette = null;
            _HeroLight.lerpTime = 0.1f;
            _HeroLight.heroLightDonut = _LightObject.FindChild("white_light_donut")!.GetComponent<SpriteRenderer>();

            var heroLight = hero.GetComponent<HeroLight>();
            _HeroLight.heroLightDonutAlphaCurve = new AnimationCurve(heroLight.heroLightDonutAlphaCurve.keys);

            var box = _LightObject.AddComponent<BoxCollider2D>();
            box.isTrigger = true;
            box.offset = Vector2.zero;
            box.size = new Vector2(1f, 1f);

            var body = _LightObject.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;

            _LightObject.SetActive(true);
            FSM = _LightObject.LocateMyFSM("color_fader");
            FSM.FsmVariables.GetFsmFloat("Down Time").Value = CameraAnimator.FadeTime;
            FSM.FsmVariables.GetFsmFloat("Up Time").Value = CameraAnimator.FadeTime;

            FadeOut(true);
        }
    }
}
