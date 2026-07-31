using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(WeaverSpeedPanel))]
internal static class WeaverSpeedPanelPatch
{
    [HarmonyPatch(nameof(WeaverSpeedPanel.RecordSpeed))]
    [HarmonyPrefix]
    public static bool RecordSpeed(WeaverSpeedPanel __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            if (__instance.lightRoutine != null)
            {
                __instance.StopCoroutine(__instance.lightRoutine);
                foreach (var light in __instance.lights)
                {
                    light.FadeToZero(__instance.lightDownDuration);
                }
            }

            __instance.StartCoroutine(__instance.Light(5));
        }

        return result;
    }
}
