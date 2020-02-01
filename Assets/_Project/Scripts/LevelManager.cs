using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool RoundOver;
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
}
