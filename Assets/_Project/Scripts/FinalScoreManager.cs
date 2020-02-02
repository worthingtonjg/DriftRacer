using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreManager : MonoBehaviour
{
    public TextMeshProUGUI PowerUps1Text;
    public TextMeshProUGUI Parts1Text;
    public TextMeshProUGUI WinBonus1Text;
    public TextMeshProUGUI Level1Text;
    public TextMeshProUGUI FinalScore1Text;

    public TextMeshProUGUI PowerUps2Text;
    public TextMeshProUGUI Parts2Text;
    public TextMeshProUGUI WinBonus2Text;
    public TextMeshProUGUI Level2Text;
    public TextMeshProUGUI FinalScore2Text;

    // Start is called before the first frame update
    void Start()
    {
        //LevelManager.Player1Parts = 10;
        //LevelManager.Player1Powerups = 20;
        //LevelManager.Player1Win = true;
        //LevelManager.Player1Score = 1000;

        //LevelManager.Player2Parts = 20;
        //LevelManager.Player2Powerups = 30;
        //LevelManager.Player2Win = !LevelManager.Player1Win;
        //LevelManager.Player2Score = 2000;

        var Parts1Score = 1000 * LevelManager.Player1Parts;
        var Powerups1Score = 1000 * LevelManager.Player1Powerups;
        var Player1Bonus = LevelManager.Player1Win ? 10000 : 0;
        var LevelScore1 = Parts1Score + Powerups1Score + Player1Bonus;

        Parts1Text.text = $"{LevelManager.Player1Parts} x 1000 = {Parts1Score}";
        PowerUps1Text.text = $"{LevelManager.Player1Powerups} x 1000 = {Powerups1Score}";
        WinBonus1Text.text = $"{Player1Bonus} pts";
        Level1Text.text = $"{LevelScore1}";
        FinalScore1Text.text = $"{LevelManager.Player1Score + LevelScore1}";

        var Parts2Score = 1000 * LevelManager.Player2Parts;
        var Powerups2Score = 1000 * LevelManager.Player2Powerups;
        var Player2Bonus = LevelManager.Player2Win ? 10000 : 0;
        var LevelScore2 = Parts2Score + Powerups2Score + Player2Bonus;

        Parts2Text.text = $"{LevelManager.Player2Parts} x 1000 = {Parts2Score}";
        PowerUps2Text.text = $"{LevelManager.Player2Powerups} x 1000 = {Powerups2Score}";
        WinBonus2Text.text = $"{Player2Bonus} pts";
        Level2Text.text = $"{LevelScore2}";
        FinalScore2Text.text = $"{LevelManager.Player2Score + LevelScore2}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
