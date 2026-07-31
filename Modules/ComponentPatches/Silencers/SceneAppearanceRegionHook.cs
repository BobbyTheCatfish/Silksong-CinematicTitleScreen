using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SceneAppearanceRegion))]
internal static class SceneAppearanceRegionPatch
{
    [HarmonyPatch(nameof(SceneAppearanceRegion.OnEnable))]
    [HarmonyPrefix]
    public static bool OnEnable()
    {
        return BasePatch.PatchResult();
    }
}
