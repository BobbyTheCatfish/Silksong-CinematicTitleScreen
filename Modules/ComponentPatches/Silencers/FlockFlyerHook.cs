using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(FlockFlyer))]
internal static class FlockFlyerPatch
{
    [HarmonyPatch(nameof(FlockFlyer.Flee))]
    [HarmonyPrefix]
    public static bool Flee()
    {
        return BasePatch.PatchResult();
    }
}
