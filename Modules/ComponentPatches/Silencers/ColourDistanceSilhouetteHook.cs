using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(ColourDistanceSilhouette))]
internal static class ColourDistanceSilhouettePatch
{
    [HarmonyPatch(nameof(ColourDistanceSilhouette.Start))]
    [HarmonyPrefix]
    public static bool Start(ColourDistanceSilhouette __instance)
    {
        __instance.startColour = __instance.tk2dSprite.color;
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(ColourDistanceSilhouette.GetT))]
    [HarmonyPrefix]
    public static bool GetT(ref float __result)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __result = 0;
        }

        return result;
    }
}
