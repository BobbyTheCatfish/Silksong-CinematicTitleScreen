using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(CurrencyObjectBase))]
internal static class CurrencyObjectBasePatch
{
    [HarmonyPatch(nameof(CurrencyObjectBase.OnFixedUpdate))]
    [HarmonyPrefix]
    public static bool OnFixedUpdate(CurrencyObjectBase __instance)
    {
        if (__instance.isAttracted)
        {
            return BasePatch.PatchResult();
        }

        return true;
    }

    [HarmonyPatch(nameof(CurrencyObjectBase.IsHeroDead), MethodType.Getter)]
    [HarmonyPrefix]
    public static bool IsHeroDead(ref bool __result)
    {
        var result = BasePatch.PatchResult();
        if (!result)
        {
            __result = false;
        }

        return result;
    }
}
