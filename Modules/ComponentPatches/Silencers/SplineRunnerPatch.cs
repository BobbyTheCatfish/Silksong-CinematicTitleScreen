using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SplineRunner))]
internal static class SplineRunnerPatch
{
    [HarmonyPatch(nameof(SplineRunner.Awake))]
    [HarmonyPrefix]
    public static bool Awake(SplineRunner __instance)
    {
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __instance.enabled = false;
        }

        return result;
    }
}
