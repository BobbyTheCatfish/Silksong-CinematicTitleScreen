using GlobalEnums;
using Silksong.UnityHelper.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen.Modules
{
    internal class SceneInsertion
    {
        public static GameObject? PlayerBox;
        public static bool Loaded => LoadedScene.HasValue;
        static SceneInstance? LoadedScene;
        public static IEnumerator LoadSceneAsync(SceneInfo scene)
        {
            if (scene.PersistantBools != null)
            {
                foreach (var data in scene.PersistantBools)
                {
                    SceneData.instance.PersistentBools.SetValue(data);
                }
            }

            if (!PlayerBox)
            {
                PlayerBox = new GameObject("Player Box", [typeof(DebugDrawColliderRuntime)]);
                PlayerBox.transform.SetParentReset(GameCameras.instance.tk2dCam.transform);
                PlayerBox.transform.SetScale2D(new Vector2(5.5f, 20));

                PlayerBox.layer = (int)PhysLayers.PLAYER;

                var body = PlayerBox.AddComponent<Rigidbody2D>();
                body.bodyType = RigidbodyType2D.Kinematic;

                var box = PlayerBox.AddComponent<BoxCollider2D>();
                box.isTrigger = true;
            }

            PlayerBox.SetActive(false);

            Debug.Log("Starting load");
            if (Loaded)
            {
                yield return RemoveScene();
                LoadedScene = null;
                yield return null;

                Debug.Log("unloading previous scene complete");
            }

            var sceneName = "Scenes/" + scene.Name;

            Debug.Log("Creating task");
            var loadOp = new AsyncOperationHandle<SceneInstance>?(ScenePreloader.TakeSceneLoadOperation(sceneName, LoadSceneMode.Additive) ?? Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, true, 100, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded));
            yield return loadOp;

            if (loadOp.Value.OperationException != null)
            {
                Debug.LogError("Additive scene load for " + scene.Name + " failed with exception:");
                Debug.LogException(loadOp.Value.OperationException);
                LoadedScene = null;
            }
            else
            {
                LoadedScene = loadOp.Value.Result;
                Debug.Log("Task complete");
                CleanupScene(LoadedScene.Value.Scene, scene);
            }

            yield return null;

            GameCameras.instance.forceCameraAspect.SetFovOffset(3, 0, AnimationCurve.Constant(0, 1, 1));
            PlayerBox.SetActive(scene.AddHero);
        }

        public static void CleanupScene(Scene scene, SceneInfo info)
        {
            if (info.DisablePaths != null)
            {

                foreach (var path in info.DisablePaths)
                {
                    var obj = scene.FindGameObject(path);
                    if (obj) obj.SetActive(false);
                }
            }

            if (info.EnablePaths != null) { 
                foreach (var path in info.EnablePaths)
                {
                    var obj = scene.FindGameObject(path);
                    if (obj) obj.SetActive(true);
                }
            }

            var cam = GameCameras.instance.tk2dCam.gameObject;
            cam.transform.SetPosition2D(new Vector2(-1000, -1000));

            var animatorObj = new GameObject("Menu Animator");
            var animator = animatorObj.AddComponent<CameraAnimator>();
            animator.SceneInfo = info;
            animator.Play();

            GameCameras.instance.sceneColorManager.UpdateScript(true);

            Debug.Log("Cleaned up!");
        }

        public static IEnumerator RemoveScene()
        {
            if (PlayerBox) PlayerBox.SetActive(false);

            if (!LoadedScene.HasValue) yield break;
            var scene = LoadedScene.Value;

            if (!scene.Scene.IsValid())
            {
                Debug.Log("Invalid scene to unload");
                yield break;
            }

            //var root = scene.Scene.GetRootGameObjects();
            //foreach (var obj in root)
            //{
            //    obj.SetActive(false);
            //}
            //GameCameras.instance.forceCameraAspect.SetFovOffset(0, 0, AnimationCurve.Constant(0, 1, 1));

            yield return null;

            var handle = Addressables.UnloadSceneAsync(scene);

            while (!handle.IsDone)
            {
                yield return null;
            }
            Debug.Log("Unloaded");

        }
    }

}
