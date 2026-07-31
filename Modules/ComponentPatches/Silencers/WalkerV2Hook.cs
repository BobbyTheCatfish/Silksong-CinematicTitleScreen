using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(WalkerV2))]
internal static class WalkerV2Patch
{
    [HarmonyPatch(nameof(WalkerV2.Start))]
    [HarmonyPrefix]
    public static bool Start(WalkerV2 __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.hero = __instance.transform;
        }

        return result;
    }
}
