
namespace CinematicTitleScreen.Modules.ComponentPatches;

internal class BasePatch
{
    public static bool PatchResult()
    {
        return GameManager.instance.IsGameplayScene();
    }
}
