using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(ParticleDamageHero))]
internal static class ParticleDamageHeroPatch
{
    [HarmonyPatch(nameof(ParticleDamageHero.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(ParticleDamageHero.OnParticleTrigger))]
    [HarmonyPrefix]
    public static bool OnParticleTrigger()
    {
        return BasePatch.PatchResult();
    }
}
