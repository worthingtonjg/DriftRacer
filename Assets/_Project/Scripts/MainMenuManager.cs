using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Player1Parts = 0;
        LevelManager.Player1Life = 20;
        LevelManager.Player2Parts = 0;
        LevelManager.Player2Life = 20;

        SceneManager.LoadScene("02GamePlay");
    }
    public void Credits()
    {

        SceneManager.LoadScene("03Credits");
    }
}
