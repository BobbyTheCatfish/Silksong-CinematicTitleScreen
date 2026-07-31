using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeroTriggerFader))]
internal static class HeroTriggerFaderPatch
{
    [HarmonyPatch(nameof(HeroTriggerFader.OnEnable))]
    [HarmonyPrefix]
    public static bool OnEnable()
    {
        return BasePatch.PatchResult();
    }
}
