using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public PauseMenuController pauseMenu;
  private SaveGameManager saveGameManager;
  public GameObject player;
  public GameObject winnerCanvas;
  // Update is called once per frame
  void Start()
  {
    Time.timeScale = 1;
    saveGameManager = gameObject.AddComponent<SaveGameManager>();
    List<SaveGame> saveGames = saveGameManager.OnRetriveSaveGame();
    if (saveGames.Count != 0 && PlayerPrefs.GetInt("isContinue") == 1)
    {
      player.transform.position = new Vector2(saveGames[^1].xPosition, saveGames[^1].yPosition);
      player.transform.Find("Player").GetComponent<HealthBar>().SetHealth(saveGames[^1].hpAmount);
      player.transform.Find("Player").GetComponent<HealthBar>().UpdateHealth(saveGames[^1].hpAmount, 1000);
    }
  }
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (!pauseMenu.isPaused)
      {
        pauseMenu.PauseGame();
      }
      else
      {
        pauseMenu.ResumeGame();
      }
    }
  }

  public void EndGame()
  {
    winnerCanvas.SetActive(true);
  }

  public void ReturnMenu() {
    SceneManager.LoadScene("Menu");
  }
}
