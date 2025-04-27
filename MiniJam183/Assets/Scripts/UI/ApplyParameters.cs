using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Linq;

public class ApplyParameters : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    Resolution[] _resolutions;

    [SerializeField] private TMP_Dropdown _fpsDropdown;

    void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.AddOptions(_resolutions.Select(res => $"{res.width}x{res.height}").Distinct().ToList());
        _fpsDropdown.AddOptions(new[] { "30", "60", "120", "144", "240" }.ToList());

        InitializeResolutionDropdown();
        InitializeFullscreenToggle();
        InitializeFpsDropdown();
    }

    private void InitializeResolutionDropdown()
    {
        var (savedWidth, savedHeight) = PlayerParamsPreferences.GetResolution();

        _resolutionDropdown.value = _resolutionDropdown.options.FindIndex(option => option.text == $"{savedWidth}x{savedHeight}");
        _resolutionDropdown.RefreshShownValue();

        Screen.SetResolution(savedWidth, savedHeight, PlayerParamsPreferences.GetFullscreen());

        _resolutionDropdown.onValueChanged.AddListener(index =>
        {
            var resolutionText = _resolutionDropdown.options[index].text;
            var dimensions = resolutionText.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
            OnResolutionChange(width, height);
        });
    }

    private void InitializeFullscreenToggle()
    {
        bool isFullscreen = PlayerParamsPreferences.GetFullscreen();

        _fullscreenToggle.isOn = isFullscreen;

        Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        _fullscreenToggle.onValueChanged.AddListener(OnFullscreenChange);
    }

    private void InitializeFpsDropdown()
    {
        int savedFps = PlayerParamsPreferences.GetFps();

        _fpsDropdown.value = _fpsDropdown.options.FindIndex(option =>
        {
            if (int.TryParse(option.text, out int fps))
            {
                return fps == savedFps;
            }
            return false;
        });
        _fpsDropdown.RefreshShownValue();

        Application.targetFrameRate = savedFps;

        _fpsDropdown.onValueChanged.AddListener(index =>
        {
            int fps = int.Parse(_fpsDropdown.options[index].text);
            OnFpsChange(fps);
        });
    }

    private void OnResolutionChange(int width, int height)
    {
        PlayerParamsPreferences.SetResolution(width, height);
        Screen.SetResolution(width, height, PlayerParamsPreferences.GetFullscreen());
    }

    private void OnFullscreenChange(bool isFullscreen)
    {
        PlayerParamsPreferences.SetFullscreen(isFullscreen);
        Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    private void OnFpsChange(int fps)
    {
        PlayerParamsPreferences.SetFps(fps);
        Application.targetFrameRate = fps;
    }
}