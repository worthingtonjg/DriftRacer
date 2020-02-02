using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool Initialized;
    public static bool RoundOver;

    public static void Init(bool forceInit = false)
    {
        if (forceInit || !Initialized)
        {
            Initialized = true;

            Player1Life = 20;
            Player1Win = false;
            Player1MaxHealth = 100;
            Player1MaxAmmo = 5;
            Player1Powerups = 0;
            Player1Score = 0;
            Player1Parts = 0;

            Player2Life = 20;
            Player2Win = false;
            Player2MaxHealth = 100;
            Player2MaxAmmo = 5;
            Player2Powerups = 0;
            Player2Score = 0;
            Player2Parts = 0;
        }
    }

    public static int Player1Ship;
    public static int Player2Ship;

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
