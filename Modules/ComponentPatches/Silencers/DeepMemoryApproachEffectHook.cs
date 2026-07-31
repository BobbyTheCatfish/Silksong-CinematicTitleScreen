using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(DeepMemoryApproachEffect))]
internal static class DeepMemoryApproachEffectPatch
{
    [HarmonyPatch(nameof(DeepMemoryApproachEffect.UpdatePosition))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
