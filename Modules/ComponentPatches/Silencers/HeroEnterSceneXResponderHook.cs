using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeroEnterSceneXResponder))]
internal static class HeroEnterSceneXResponderPatch
{
    [HarmonyPatch(nameof(HeroEnterSceneXResponder.Awake))]
    [HarmonyPrefix]
    public static bool Awake()
    {
        return BasePatch.PatchResult();
    }
}
