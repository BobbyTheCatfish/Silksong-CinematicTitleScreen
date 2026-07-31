using HarmonyLib;

namespace CinematicTitleScreen.Modules.ComponentPatches.Silencers;

[HarmonyPatch(typeof(MazeCorpseSpawner))]
internal static class MazeCorpseSpawnerPatch
{
    [HarmonyPatch(nameof(MazeCorpseSpawner.Start))]
    [HarmonyPrefix]
    public static bool Start()
    {
        return BasePatch.PatchResult();
    }
}
