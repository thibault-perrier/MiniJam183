using UnityEngine;

public static class PlayerParamsPreferences
{
    public static string ResolutionWidthPP = "ResolutionWidth";
    public static string ResolutionHeightPP = "ResolutionHeight";
    public static string FullscreenPP = "Fullscreen";
    public static string FpsPP = "Fps";

    public static string PlayerPrefsMasterVol = "MasterVolume";
    public static string PlayerPrefsMusicVol = "MusicVolume";
    public static string PlayerPrefsSFXVol = "SFXVolume";

    public static string PlayerPrefsMasterVolumeMixer = "MasterVolume";
    public static string PlayerPrefsMusicVolumeMixer = "MusicVolume";
    public static string PlayerPrefsSFXVolumeMixer = "SFXVolume";

    public static void SetResolution(int width, int height)
    {
        PlayerPrefs.SetInt(ResolutionWidthPP, width);
        PlayerPrefs.SetInt(ResolutionHeightPP, height);
    }

    public static (int width, int height) GetResolution()
    {
        int width = PlayerPrefs.GetInt(ResolutionWidthPP, Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt(ResolutionHeightPP, Screen.currentResolution.height);
        return (width, height);
    }

    public static void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt(FullscreenPP, isFullscreen ? 1 : 0);
    }

    public static bool GetFullscreen()
    {
        return PlayerPrefs.GetInt(FullscreenPP, 0) == 1;
    }

    public static void SetFps(int fps)
    {
        PlayerPrefs.SetInt(FpsPP, fps);
    }

    public static int GetFps()
    {
        return PlayerPrefs.GetInt(FpsPP, 60);
    }
}
