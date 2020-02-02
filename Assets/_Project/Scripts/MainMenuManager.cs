using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Init(true);
        SceneManager.LoadScene("01MainMenuA");
    }
    public void Credits()
    {

        SceneManager.LoadScene("03Credits");
    }
}
