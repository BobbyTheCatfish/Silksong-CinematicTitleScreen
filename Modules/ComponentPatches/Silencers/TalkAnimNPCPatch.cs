using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(TalkAnimNPC))]
internal static class TalkAnimNPCPatch
{
    [HarmonyPatch(nameof(TalkAnimNPC.StartAnimation))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
