
using HarmonyLib;
using UnityEngine;

namespace CinematicTitleScreen.Modules.ComponentPatches;


//[HarmonyPatch(typeof(AlertRange))]
//internal class AlertRangePatch
//{

//    [HarmonyPatch(nameof(AlertRange.OnInsideStateChanged))]
//    [HarmonyPostfix]
//    private static void Awake(AlertRange __instance)
//    {
//        if (!BasePatch.PatchResult())
//        {
//            Debug.Log(__instance.isHeroInRange);
//        }
//    }
//}


[HarmonyPatch(typeof(TrackTriggerObjects))]
static class TrackTriggerObjectsPatch
{
    [HarmonyPatch(nameof(TrackTriggerObjects.OnEnable))]
    [HarmonyPostfix]
    private static void OnEnable(TrackTriggerObjects __instance)
    {
        if (!BasePatch.PatchResult())
        {
            __instance.OnHeroInPosition(false);
        }
    }
}