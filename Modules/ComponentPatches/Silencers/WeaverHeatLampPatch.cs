using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(WeaverHeatLamp))]
internal static class WeaverHeatLampPatch
{
    [HarmonyPatch(nameof(WeaverHeatLamp.OnEnable))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();   
    }
}
