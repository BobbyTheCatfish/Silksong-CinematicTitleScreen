using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches
{
    [HarmonyPatch(typeof(CameraManagerReference))]
    internal static class GameCameraPatch
    {
        [HarmonyPatch(nameof(CameraManagerReference.DoShake))]
        [HarmonyPrefix]
        public static bool A()
        {
            return BasePatch.PatchResult();            
        }
    }
}
