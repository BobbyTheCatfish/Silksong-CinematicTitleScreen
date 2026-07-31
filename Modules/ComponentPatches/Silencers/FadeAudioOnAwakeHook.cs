using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(FadeAudioOnAwake))]
internal static class FadeAudioOnAwakePatch
{
    [HarmonyPatch(nameof(FadeAudioOnAwake.Update))]
    [HarmonyPrefix]
    public static bool Update()
    {
        return BasePatch.PatchResult();
    }
}
