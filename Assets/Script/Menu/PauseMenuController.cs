using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuController : MonoBehaviour
{
  public GameObject mainMenu;
  public GameObject settingMenu;
  public GameObject canvas;
  public GameObject character;
  public bool isPaused = false;
  private SaveGameManager saveGameManager;
  void Awake()
  {
    saveGameManager = gameObject.AddComponent<SaveGameManager>();
  }
  public void ResumeGame()
  {
    Time.timeScale = 1;
    isPaused = false;
    canvas.SetActive(false);
  }
  public void PauseGame()
  {
    Time.timeScale = 0;
    isPaused = true;
    canvas.SetActive(true);
  }
  public void OpenSetting()
  {
    mainMenu.SetActive(false);
    settingMenu.SetActive(true);
  }
  public void BackSetting()
  {
    mainMenu.SetActive(true);
    settingMenu.SetActive(false);
  }
  public void ExitMenu()
  {
    SaveGame save = new(
      DateTime.Now.ToString(),
      character.transform.position.x,
      character.transform.position.y,
      character.GetComponent<HealthBar>().currentHealth,
      SceneManager.GetActiveScene().name
    );
    saveGameManager.OnSaveGame(save);
    SceneManager.LoadScene("Menu");
  }
  public void ExitGame()
  {
    SaveGame save = new(
      DateTime.Now.ToString(),
      character.transform.position.x,
      character.transform.position.y,
      character.GetComponent<HealthBar>().currentHealth,
      SceneManager.GetActiveScene().ToString()
    );
    saveGameManager.OnSaveGame(save);
    Application.Quit();
  }

}
