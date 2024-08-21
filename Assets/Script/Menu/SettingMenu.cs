using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;
using UnityEngine.Audio;
using TMPro;
public class VolumeSetting : MonoBehaviour
{
  public TextMeshProUGUI fullScreenText;
  public AudioMixer masterVolume;
  public void SetVolume(float volume)
  {
    masterVolume.SetFloat("volume", volume);
  }

  public void SetQuality(int qualityIndex)
  {
    QualitySettings.SetQualityLevel(qualityIndex);
  }

  public void SetFullscreen()
  {
    if (fullScreenText.text == "Fullscreen")
    {
      fullScreenText.text = "Windowed";
      Screen.SetResolution(1280, 720, false);
    }
    else
    {
      fullScreenText.text = "Fullscreen";
      Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
  }
}
