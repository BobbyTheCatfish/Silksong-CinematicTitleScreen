using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(CaravanMusicControl))]
internal static class CaravanMusicControlPatch
{
    [HarmonyPatch(nameof(CaravanMusicControl.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
