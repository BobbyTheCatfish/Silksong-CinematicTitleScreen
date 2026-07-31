using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches;

[HarmonyPatch(typeof(TriggerEnterEvent))]
internal static class TriggerEnterEventPatch
{
    [HarmonyPatch(nameof(TriggerEnterEvent.Start))]
    [HarmonyPrefix]
    public static bool Start(TriggerEnterEvent __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.HeroInPosition();
        }

        return result;
    }

    [HarmonyPatch(nameof(TriggerEnterEvent.ShouldDelay), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool ShouldDelay(ref bool __result)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __result = false;
        }

        return result;
    }
}
