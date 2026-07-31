using HarmonyLib;
using UnityEngine;
using UnityEngine.Audio;

namespace CinematicTitleScreen.Modules.ComponentPatches;

[HarmonyPatch(typeof(AudioManager))]
internal static class AudioManagerPatch
{
    public static bool HasApplied = false;

    [HarmonyPatch(nameof(AudioManager.StopAndClearMusic))]
    [HarmonyPrefix]
    public static bool StopAndClearMusic()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AudioManager.TransitionToCurrentMusic))]
    [HarmonyPrefix]
    public static bool TransitionToCurrentMusic()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AudioManager.ApplyMusicCue))]
    [HarmonyPrefix]
    public static bool ApplyMusicCue()
    {
        if (HasApplied) return BasePatch.PatchResult();
        HasApplied = true;

        return true;
    }

    [HarmonyPatch(nameof(AudioManager.ApplyMusicSnapshot), [typeof(AudioMixerSnapshot), typeof(float), typeof(float), typeof(bool)] )]
    [HarmonyPrefix]
    public static bool ApplyMusicSnapshot(bool blockMusicMarker)
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AudioManager.ApplyMusicSnapshot), [typeof(AudioMixerSnapshot), typeof(float), typeof(float)])]
    [HarmonyPrefix]
    public static bool ApplyMusicSnapshot()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AudioManager.AddMusicMarker))]
    [HarmonyPrefix]
    public static bool AddMusicMarker()
    {
        return BasePatch.PatchResult();
    }

    [HarmonyPatch(nameof(AudioManager.RemoveMusicMarker))]
    [HarmonyPrefix]
    public static bool RemoveMusicMarker()
    {
        return BasePatch.PatchResult();
    }
}


[HarmonyPatch(typeof(PauseAudioListener))]
internal static class PauseAudioListenerPatch
{
    [HarmonyPatch(nameof(PauseAudioListener.OnEnter))]
    [HarmonyPrefix]
    public static bool OnEnter(PauseAudioListener __instance)
    {
        Debug.Log("enter called");
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __instance.Finish();
        }

        return result;
    }

    [HarmonyPatch(nameof(PauseAudioListener.OnExit))]
    [HarmonyPrefix]
    public static bool OnExit(PauseAudioListener __instance)
    {
        Debug.Log("exit called");
        var result = BasePatch.PatchResult();

        if (!result)
        {
            __instance.Finish();
        }

        return result;
    }
}