using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Player1Life = 20;
        LevelManager.Player1Win = false;
        LevelManager.Player1MaxHealth = 100;
        LevelManager.Player1MaxAmmo = 5;
        LevelManager.Player1Powerups = 0;
        LevelManager.Player1Score = 0;
        LevelManager.Player1Parts = 0;

        LevelManager.Player2Life = 20;
        LevelManager.Player2Win = false;
        LevelManager.Player2MaxHealth = 100;
        LevelManager.Player2MaxAmmo = 5;
        LevelManager.Player2Powerups = 0;
        LevelManager.Player2Score = 0;
        LevelManager.Player2Parts = 0;

        SceneManager.LoadScene("01MainMenuA");
    }
    public void Credits()
    {

        SceneManager.LoadScene("03Credits");
    }
}
