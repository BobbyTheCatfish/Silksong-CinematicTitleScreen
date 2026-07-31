using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(LookAnimNPC))]
internal static class LookAnimNPCPatch
{
    [HarmonyPatch(nameof(LookAnimNPC.Start))]
    [HarmonyPrefix]
    public static void Start(LookAnimNPC __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.turnOnInteract = true;
        }
    }
}
