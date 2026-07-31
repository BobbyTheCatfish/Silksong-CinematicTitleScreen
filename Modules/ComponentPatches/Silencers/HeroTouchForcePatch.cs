using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeroTouchForce))]
internal static class HeroTouchForcePatch
{
    [HarmonyPatch(nameof(HeroTouchForce.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(HeroTouchForce.Update))]
    [HarmonyPrefix]
    public static bool Update()
    {
        return BasePatch.PatchResult();
    }
}
