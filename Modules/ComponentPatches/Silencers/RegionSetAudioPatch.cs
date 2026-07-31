using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(RegionSetAudio))]
internal static class RegionSetAudioPatch
{
    [HarmonyPatch(nameof(RegionSetAudio.OnEnable))]
    [HarmonyPrefix]
    public static bool OnEnable()
    {
        return BasePatch.PatchResult();
    }
}
