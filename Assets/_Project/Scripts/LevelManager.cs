using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool RoundOver;

    public static float Player1MaxHealth;
    public static float Player2MaxHealth;

    public static float Player1MaxAmmo;
    public static float Player2MaxAmmo;

    public static int Player1Life;
    public static int Player2Life;

    public static int Player1Parts;
    public static int Player2Parts;

    public static int Player1Powerups;
    public static int Player2Powerups;

    public static int Player1Score;
    public static int Player2Score;

    public static bool Player1Win;
    public static bool Player2Win;

    public string NextLevel;
    
    private static string _nextLevel;

    private void Start()
    {
        _nextLevel = NextLevel;
    }

    public static void LoadNext()
    {
        SceneManager.LoadScene(_nextLevel);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
