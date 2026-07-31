using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(Remasker))]
internal static class RemaskerPatch
{
    [HarmonyPatch(nameof(Remasker.Start))]
    [HarmonyPrefix]
    public static bool Start(Remasker __instance)
    {
        __instance.gm = GameManager.instance;

        return BasePatch.PatchResult();
    }
}
