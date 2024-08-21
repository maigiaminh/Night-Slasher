using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuController : MonoBehaviour
{
  SaveGameManager saveGameManager;
  public GameObject continueButton;
  List<SaveGame> saveGames;
  void Awake()
  {
    saveGameManager = gameObject.AddComponent<SaveGameManager>();
    saveGames = saveGameManager.OnRetriveSaveGame();

    if (saveGames.Count == 0)
    {
      continueButton.GetComponent<Button>().enabled = false;
      continueButton.GetComponent<TextMeshProUGUI>().color = Color.grey;
    } 
  }
  public void PlayNew()
  {
    SceneManager.LoadScene("map n+1");
    PlayerPrefs.SetInt("isContinue", 0);
  }
  public void PlayContinue()
  {
    SceneManager.LoadScene(saveGames[^1].map);
    PlayerPrefs.SetInt("isContinue", 1);
  }
}