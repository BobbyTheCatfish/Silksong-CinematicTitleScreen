using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(HeroFallParticle))]
internal static class HeroFallParticlePatch
{
    [HarmonyPatch(nameof(HeroFallParticle.LateUpdate))]
    [HarmonyPrefix]
    public static bool LateUpdate()
    {
        return BasePatch.PatchResult();
    }
}
