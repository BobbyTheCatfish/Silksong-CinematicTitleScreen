using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(ClockworkHatchling))]
internal static class ClockworkHatchlingPatch
{
    [HarmonyPatch(nameof(ClockworkHatchling.OnEnable))]
    [HarmonyPrefix]
    public static bool OnEnable()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(ClockworkHatchling.FixedUpdate))]
    [HarmonyPrefix]
    public static bool Start(ClockworkHatchling __instance)
    {
        if (__instance.CurrentState == ClockworkHatchling.State.Follow)
        {
            return BasePatch.PatchResult();
        }

        return true;
    }
}
