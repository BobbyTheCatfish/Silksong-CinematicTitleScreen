using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen.Modules
{
    internal static class MenuCleaner
    {
        public static void CleanMenu(Scene scene)
        {
            if (GameManager.instance.GameState != GlobalEnums.GameState.MAIN_MENU) return;

            GameCameras.instance.forceCameraAspect.SetFovOffset(3, 0, AnimationCurve.Constant(0, 1, 1));

            var objects = scene.GetRootGameObjects();

            var background = objects.FirstOrDefault(o => o.name == "Menu_Styles");
            if (background)
            {
                background.SetActive(false);
            }

            var blur = objects.FirstOrDefault(o => o.name == "BlurPlane");
            if (blur)
            {
                blur.transform.SetPositionZ(27);
            }

            var logo = scene.GetRootGameObjects().FirstOrDefault(g => g.name == "LogoTitle");
            if (logo)
            {
                logo.AddComponentIfNotPresent<PositionToCamera>();
            }
        }
    }
}
