using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SceneColorManager))]
internal static class SceneColorManagerPatch
{
    [HarmonyPatch(nameof(SceneColorManager.SceneInit))]
    [HarmonyPrefix]
    public static bool SceneInit()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(SceneColorManager.UpdateScriptParameters))]
    [HarmonyPrefix]
    public static bool UpdateScriptParameters()
    {
        return BasePatch.PatchResult();
    }
}
