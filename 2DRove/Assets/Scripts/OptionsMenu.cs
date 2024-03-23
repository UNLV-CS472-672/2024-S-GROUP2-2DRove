using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;               // Audio mixer to control the volume of the game

    public TMP_Dropdown resolutionDropdown;     // Dropdown to select the resolution of the game

    Resolution[] resolutions;                   // Array to store all the resolutions that the screen supports

    void Start(){
        // Set the resolution dropdown to the current resolution
        setResolution();
        
    }

    // Set the volume of the audio mixer
    public void setVolume(float vol){
        audioMixer.SetFloat("MasterVolume", vol);
    }

    // Set the full screen mode for the game
    public void setFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
  
    // Set the screen resolution of the game
    public void setResolution(){
        resolutions = Screen.resolutions;           // Get all the resolutions that the screen supports

        resolutionDropdown.ClearOptions();          // Clear the current options in the dropdown

        List<string> options = new List<string>();  // Create a list of options for the dropdown

        int currentResolutionIndex = 0;             // Index of the current resolution

        // Loop through all the resolutions and add them to the options list
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        // Add the options to the dropdown and set the current resolution
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
