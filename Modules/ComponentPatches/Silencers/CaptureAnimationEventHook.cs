using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(CaptureAnimationEvent))]
internal static class CaptureAnimationEventPatch
{
    [HarmonyPatch(nameof(CaptureAnimationEvent.Start))]
    [HarmonyPrefix]
    public static bool Start(CaptureAnimationEvent __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.playerData = PlayerData.instance;
        }

        return result;
    }

    [HarmonyPatch(nameof(CaptureAnimationEvent.UpdateBlueHealth))]
    [HarmonyPrefix]
    public static bool UpdateBlueHealth()
    {
        return BasePatch.PatchResult();
    }
}
