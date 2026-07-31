using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(VectorCurveAnimator))]
internal static class VectorCurveAnimatorPatch
{
    [HarmonyPatch(nameof(VectorCurveAnimator.StartAnimationFlipHeroSideX))]
    [HarmonyPrefix]
    public static bool StartAnimationFlipHeroSideX()
    {
        return BasePatch.PatchResult();
    }
}
