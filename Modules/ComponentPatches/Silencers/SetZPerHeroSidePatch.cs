using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SetZPerHeroSide))]
internal static class SetZPerHeroSidePatch
{
    [HarmonyPatch(nameof(SetZPerHeroSide.LateUpdate))]
    [HarmonyPrefix]
    public static bool LateUpdate()
    {
        return BasePatch.PatchResult();
    }
}
