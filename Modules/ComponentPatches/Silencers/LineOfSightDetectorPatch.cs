using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(LineOfSightDetector))]
internal static class LineOfSightDetectorPatch
{
    [HarmonyPatch(nameof(LineOfSightDetector.Update))]
    [HarmonyPrefix]
    public static bool Update()
    {
        return BasePatch.PatchResult();
    }
}
