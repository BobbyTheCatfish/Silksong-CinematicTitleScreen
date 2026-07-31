using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeartPieceOrb))]
internal static class HeartPieceOrbPatch
{
    [HarmonyPatch(nameof(HeartPieceOrb.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
