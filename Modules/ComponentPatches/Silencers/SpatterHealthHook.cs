using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SpatterHealth))]
internal static class SpatterHealthPatch
{
    [HarmonyPatch(nameof(SpatterHealth.FixedUpdate))]
    [HarmonyPrefix]
    public static bool FixedUpdate()
    {
        return BasePatch.PatchResult();
    }
}
