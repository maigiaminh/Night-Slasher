using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
  public GameObject mainMenu;
  public GameObject settingMenu;
  public GameObject creditMenu;
  public GameObject playMenu;
  void Awake() {
    Screen.fullScreen = true;
  }
  public void OpenPlay() {
    mainMenu.SetActive(false);
    playMenu.SetActive(true);
  }
  public void BackPlay()
  {
    mainMenu.SetActive(true);
    playMenu.SetActive(false);
  }
  public void OpenSetting() {
    mainMenu.SetActive(false);
    settingMenu.SetActive(true);
  }
  public void BackSetting()
  {
    mainMenu.SetActive(true);
    settingMenu.SetActive(false);
  }
  public void OpenCredit()
  {
    mainMenu.SetActive(false);
    creditMenu.SetActive(true);
  }

  public void BackCredit()
  {
    mainMenu.SetActive(true);
    creditMenu.SetActive(false);
  }

  public void ExitGame() {
    Application.Quit();
  }

}
