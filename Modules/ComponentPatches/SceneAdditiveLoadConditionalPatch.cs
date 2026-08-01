using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;



namespace CinematicTitleScreen.Modules.ComponentPatches;

/// <summary>
/// This patch fixes attempts to unload scenes that are already unloaded.
///
/// Test:
/// - Use bell beast scene
/// - Load into save with bell beast defeated
/// - Exit to menu
/// - Re-enter the save
/// - Move to another scene
/// </summary>
[HarmonyPatch(typeof(SceneAdditiveLoadConditional))]
internal class SceneAdditiveLoadConditionalPatch
{
    [HarmonyPatch(nameof(SceneAdditiveLoadConditional.Unload), [])]
    [HarmonyPrefix]
    static bool Unload(SceneAdditiveLoadConditional __instance)
    {
        if (!__instance.sceneLoaded) return false;
        if (__instance.loadOp != null)
        {
            if (!__instance.loadOp.Value.Result.Scene.IsValid()) return false;
        }

        return true;
    }

    [HarmonyPatch(nameof(SceneAdditiveLoadConditional.Unload), [typeof(Scene), typeof(List<AsyncOperationHandle<SceneInstance>>)])]
    [HarmonyPrefix]
    static void Unload2(SceneAdditiveLoadConditional __instance)
    {
        SceneAdditiveLoadConditional._additiveSceneLoads.RemoveAll(l => !l || !l.gameObject);
    }
}
