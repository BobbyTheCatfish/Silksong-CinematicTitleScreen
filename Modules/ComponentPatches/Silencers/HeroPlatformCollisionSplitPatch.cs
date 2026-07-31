using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeroPlatformCollisionSplit))]
internal static class HeroPlatformCollisionSplitPatch
{
    [HarmonyPatch(nameof(HeroPlatformCollisionSplit.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
