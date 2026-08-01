using BepInEx;
using CinematicTitleScreen.Data;
using CinematicTitleScreen.Modules;
using HarmonyLib;
using Silksong.AssetHelper.ManagedAssets;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen
{
    // TODO - adjust the plugin guid as needed
    [BepInAutoPlugin(id: "io.github.bobbythecatfish.cinematictitlescreen")]
    public partial class CinematicTitleScreenPlugin : BaseUnityPlugin
    {
        internal static CinematicTitleScreenPlugin instance;
        internal static SceneInfo SceneInfo;

        private void Awake()
        {
            instance = this;
            // Put your initialization logic here
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");

#if DEBUG
            IntroSceneData.SceneIndexOverride = IntroSceneData.Scenes.Count - 1;
#endif

            SceneInfo = IntroSceneData.GetRandomScene();

            var asset = ManagedAsset<GameObject>.FromNonSceneAsset("Assets/Prefabs/Heroes/Hero_Hornet.prefab", "heroloading_assets_all");
            HeroLightReplacement.SetHeroAsset(asset);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "CinematicTitleScreen");
            SceneManager.activeSceneChanged += OnSceneChange;

        }

#if DEBUG
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Slash) && SceneInsertion.Loaded)
            {
                CameraAnimator.Instance.StopAllCoroutines();
                //DestroyImmediate(CameraAnimator.Instance);

                IntroSceneData.SceneIndexOverride++;
                if (IntroSceneData.SceneIndexOverride == IntroSceneData.Scenes.Count) IntroSceneData.SceneIndexOverride = 0;

                SceneInfo = IntroSceneData.GetRandomScene();

                var scene = SceneManager.GetActiveScene();
                OnSceneChange(scene, scene);
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                UnityExplorer.InspectorManager.Inspect(GameCameras.instance.tk2dCam.gameObject);
                UnityExplorer.UI.UIManager.GetPanel(UnityExplorer.UI.UIManager.Panels.ObjectExplorer).SetActive(false);
                UnityExplorer.UI.UIManager.GetPanel(UnityExplorer.UI.UIManager.Panels.Clipboard).SetActive(false);
                UnityExplorer.UI.UIManager.GetPanel(UnityExplorer.UI.UIManager.Panels.ConsoleLog).SetActive(false);
            }
        }
#endif

        private void OnSceneChange(Scene oldScene, Scene scene)
        {
            //if (Loaded)
            //{
            //    SceneInsertion.RemoveScene(LoadedScene);
            //    Loaded = false;
            //}

            if (scene.name != Common.MenuSceneName)
            {
                Debug.Log("Not menu");

                if (SceneInsertion.Loaded)
                {
                    StartCoroutine(SceneInsertion.RemoveScene());
                }

                return;
            }

            if (SceneInfo.PlayerFlags != null)
            {
                foreach (var flag in SceneInfo.PlayerFlags)
                {
                    PlayerData.instance.SetBool(flag, true);
                }
            }

            StartCoroutine(SceneInsertion.LoadSceneAsync(SceneInfo));

            IEnumerator CleanRoutine()
            {
                yield return null;
                MenuCleaner.CleanMenu(scene);

                yield return StartCoroutine(HeroLightReplacement.CreateLight());
            }

            StartCoroutine(CleanRoutine());

        }
    }
}
