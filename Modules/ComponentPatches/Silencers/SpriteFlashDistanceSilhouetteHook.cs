using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SpriteFlashDistanceSilhouette))]
internal static class SpriteFlashDistanceSilhouettePatch
{
    [HarmonyPatch(nameof(SpriteFlashDistanceSilhouette.Start))]
    [HarmonyPrefix]
    public static bool Start(SpriteFlashDistanceSilhouette __instance)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __instance.hero = __instance.transform;
        }

        return result;
    }
}
