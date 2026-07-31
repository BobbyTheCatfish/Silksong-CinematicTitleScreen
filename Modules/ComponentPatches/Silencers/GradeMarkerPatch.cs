using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(GradeMarker))]
internal static class GradeMarkerPatch
{
    [HarmonyPatch(nameof(GradeMarker.Start))]
    [HarmonyPrefix]
    public static bool Start(GradeMarker __instance)
    {
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __instance.enabled = false;
        }

        return result;
    }
}
