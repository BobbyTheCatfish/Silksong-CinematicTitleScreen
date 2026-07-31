using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(SavedItem))]
internal static class SavedItemPatch
{
    [HarmonyPatch(nameof(SavedItem.TryGet))]
    [HarmonyPrefix]
    public static bool TryGet(ref bool __result)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __result = false;
        }

        return result;
    }
}
