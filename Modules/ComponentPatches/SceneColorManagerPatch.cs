using HarmonyLib;
using UnityEngine;

namespace CinematicTitleScreen.Modules.ComponentPatches;

/// <summary>
/// This patch fixes HeroLight related color management
/// </summary>
[HarmonyPatch(typeof(SceneColorManager))]
internal static class SceneColorManagerPatch
{
    [HarmonyPatch(nameof(SceneColorManager.SceneInit))]
    [HarmonyPrefix]
    public static bool SceneInit(SceneColorManager __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            //__instance.StartBufferActive = true;
            __instance.MarkerActive = true;
            __instance.UpdateScript(true);
            //__instance.FinishBufferPeriod();
        }

        return result;
    }

    [HarmonyPatch(nameof(SceneColorManager.UpdateScriptParameters))]
    [HarmonyPostfix]
    public static void UpdateScriptParameters(SceneColorManager __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result && __instance.hasCurvesScript)
        {
            if (HeroLightReplacement.HeroLight)
            {
                HeroLightReplacement.HeroLight.BaseColor = Color.Lerp(__instance.HeroLightColorA, __instance.HeroLightColorB, __instance.Factor);
            }
        }
    }
}
