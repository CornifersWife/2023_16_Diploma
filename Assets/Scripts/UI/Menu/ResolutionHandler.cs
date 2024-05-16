using System.Collections.Generic;
using UnityEngine;

public static class ResolutionHandler {
    private static Resolution[] resolutions;

    public static List<string> Options { get; private set; }
    public static int CurrentResolutionIndex { get; private set; }

    public static void SetUpResolutions() {
        resolutions = Screen.resolutions;
        Options = new List<string>();
        CurrentResolutionIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            Options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) 
                CurrentResolutionIndex = i;
        }
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
    }

    public static void ChangeResolution(int resolutionIndex) {
        CurrentResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        if(!resolution.Equals(Screen.currentResolution)) 
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}