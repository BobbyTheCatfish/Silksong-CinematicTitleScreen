using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(AreaTitleController))]
internal static class AreaTitleControllerPatch
{
    [HarmonyPatch(nameof(AreaTitleController.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AreaTitleController.Play))]
    [HarmonyPrefix]
    public static bool Play()
    {
        return BasePatch.PatchResult();
    }
}
