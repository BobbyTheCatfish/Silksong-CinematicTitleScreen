using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(TrapBridgeFlip))]
internal static class TrapBridgeFlipPatch
{
    [HarmonyPatch(nameof(TrapBridgeFlip.IsHeroOnLeft))]
    [HarmonyPrefix]
    public static bool IsHeroOnLeft()
    {
        return BasePatch.PatchResult();
    }
}
