using HarmonyLib;
using UnityEngine;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(FadeUpWhileIntersecting))]
internal static class FadeUpWhileIntersectingPatch
{
    [HarmonyPatch(nameof(FadeUpWhileIntersecting.GetTarget))]
    [HarmonyPrefix]
    public static bool GetTarget(ref Transform __result)
    {
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __result = null;
        }

        return result;
    }
}
