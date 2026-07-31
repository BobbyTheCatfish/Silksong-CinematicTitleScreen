using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches;

[HarmonyPatch(typeof(WeaverLift))]
internal static class WeaverLiftPatch
{
    [HarmonyPatch(nameof(WeaverLift.IsAvailable), MethodType.Getter)]
    [HarmonyPostfix]
    public static void IsAvailable(ref bool __result)
    {
        if (!BasePatch.PatchResult())
        {
            __result = true;
        }
    }

    [HarmonyPatch(nameof(WeaverLift.Start))]
    [HarmonyPostfix]
    public static void Start(WeaverLift __instance)
    {
        if (!BasePatch.PatchResult())
        {
            __instance.SetActive(true, true);
        }
    }
}
