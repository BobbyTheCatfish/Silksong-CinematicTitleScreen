using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(Trapdoor))]
internal static class TrapdoorPatch
{
    [HarmonyPatch(nameof(Trapdoor.Awake))]
    [HarmonyPrefix]
    public static bool Awake(Trapdoor __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.positiveAnims.UpdateAnimHashes();
            __instance.negativeAnims.UpdateAnimHashes();
        }

        return result;
    }
}
