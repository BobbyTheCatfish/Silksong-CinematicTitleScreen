using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(PositionToHeroClamped))]
internal static class PositionToHeroClampedPatch
{
    [HarmonyPatch(nameof(PositionToHeroClamped.Start))]
    [HarmonyPrefix]
    public static bool Start(PositionToHeroClamped __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            if (SceneInsertion.PlayerBox) __instance.hero = SceneInsertion.PlayerBox.transform;
            else __instance.hero = __instance.gameObject.transform;
        }

        return result;
    }
}
