using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(EnemyHitEffectsProfile))]
internal static class EnemyHitEffectsProfilePatch
{
    [HarmonyPatch(nameof(EnemyHitEffectsProfile.Get))]
    [HarmonyPrefix]
    public static bool Get(EnemyHitEffectsProfile __instance, ref EnemyHitEffectsProfile __result)
    {
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __result = __instance;
        }

        return result;
    }
}
