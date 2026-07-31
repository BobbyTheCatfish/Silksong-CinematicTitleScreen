using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(ManualLift))]
internal static class ManualLiftPatch
{
    [HarmonyPatch(nameof(ManualLift.SetStartPos))]
    [HarmonyPrefix]
    public static void SetStartPos(ManualLift __instance)
    {
        if (!BasePatch.PatchResult())
        {
            __instance.isUnlocked = false;
        }
    }
}
