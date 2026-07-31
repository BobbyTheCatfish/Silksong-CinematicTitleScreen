using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(BellBench))]
internal static class BellBenchPatch
{
    [HarmonyPatch(nameof(BellBench.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
