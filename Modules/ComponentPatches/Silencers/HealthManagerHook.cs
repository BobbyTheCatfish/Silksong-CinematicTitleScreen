using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HealthManager))]
internal static class HealthManagerPatch
{
    [HarmonyPatch(nameof(HealthManager.TakeDamage))]
    [HarmonyPrefix]
    public static bool TakeDamage()
    {
        return BasePatch.PatchResult();
    }
}
