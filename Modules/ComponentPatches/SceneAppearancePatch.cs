using HarmonyLib;
using Silksong.UnityHelper.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CinematicTitleScreen.Modules.ComponentPatches;

/// <summary>
/// This patch fixes HeroLight related color management
/// </summary>
[HarmonyPatch(typeof(SceneAppearanceRegion))]
internal static class SceneAppearanceRegionPatch
{
    [HarmonyPatch(nameof(SceneAppearanceRegion.OnEnable))]
    [HarmonyPrefix]
    public static bool OnEnable(SceneAppearanceRegion __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.HeroInPosition(false);
        }

        return result;
    }

    [HarmonyPatch(nameof(SceneAppearanceRegion.OnTriggerExit2D))]
    [HarmonyPostfix]
    public static void OnExit(SceneAppearanceRegion __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            var scene = SceneManager.GetActiveScene();
            var manager = scene.FindGameObject("_SceneManager");
            if (manager)
            {
                var csm = manager.GetComponent<CustomSceneManager>();
                csm.UpdateScene();
            }
            CustomSceneManager.SetLighting(__instance.ambientLightColor, __instance.ambientLightIntensity);
        }
    }
}

/// <summary>
/// This patch fixes HeroLight related color management
/// </summary>
[HarmonyPatch(typeof(CustomSceneManager))]
internal static class CustomSceneManagerPatch
{
    [HarmonyPatch(nameof(CustomSceneManager.UpdateScene))]
    [HarmonyPrefix]
    public static bool UpdateScenePre(CustomSceneManager __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result && !__instance.IsGradeOverridden)
        {
            var light = HeroLightReplacement.HeroLight;
            if (light)
            {
                light.BaseColor = __instance.heroLightColor;
                light.Alpha = 1f;
                light.UpdateColor(true);
            }

            __instance.gc.sceneColorManager.HeroLightColorA = __instance.heroLightColor;
            var offset = __instance.heroSaturationOffset + 0.5f;
            Debug.Log(Shader.GetGlobalFloat(CustomSceneManager._desaturationPropId));
            Shader.SetGlobalFloat(CustomSceneManager._desaturationPropId, offset);
        }

        Debug.Log(Shader.GetGlobalFloat(CustomSceneManager._desaturationPropId));
        return true;
    }
}

/// <summary>
/// This patch fixes HeroLight related color management
/// </summary>
[HarmonyPatch(typeof(DarknessRegion))]
internal static class DarknessRegionPatch
{
    [HarmonyPatch(nameof(DarknessRegion.GetDarknessLevel))]
    [HarmonyPrefix]
    public static bool UpdateScene(ref int __result)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __result = 0;            
        }

        return result;
    }
}

