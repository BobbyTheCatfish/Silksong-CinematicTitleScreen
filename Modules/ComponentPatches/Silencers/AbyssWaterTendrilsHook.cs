using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(AbyssWaterTendrils))]
internal static class AbyssWaterTendrilsPatch
{
    [HarmonyPatch(nameof(AbyssWaterTendrils.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AbyssWaterTendrils.Update))]
    [HarmonyPrefix]
    public static bool Update()
    {
        return BasePatch.PatchResult();
    }
}
