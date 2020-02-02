using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateManager : MonoBehaviour
{

    public enum EnumShipType
    {
        Player1,
        Player2
    }

    public int PartScore;
    public int PowerUps;
    public int PowerUpScore;
    public int LevelScore;
    public int PlayerLife;
    public int PlayerParts;
    public int PlayerBonus;
    public int PlayerTotal;
    public float PlayerMaxHealth;
    public float PlayerMaxAmmo;

    public TextMeshProUGUI PartsText;
    public TextMeshProUGUI PowerUpsText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI TotalCountText;
    public TextMeshProUGUI PlayerLifeText;
    public TextMeshProUGUI PlayerPartsText;
    public TextMeshProUGUI PlayerMaxHealthText;
    public TextMeshProUGUI PlayerMaxAmmoText;
    public TextMeshProUGUI WinBonusText;

    public EnumShipType shipType;

    TextMeshProUGUI powerups;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: REMOVE THESE - FAKE DATA
        //LevelManager.Player1Parts = 3;
        //LevelManager.Player1Powerups = 5;
        //LevelManager.Player1Win = false;
        //LevelManager.Player1Life = 15;
        //LevelManager.Player1MaxHealth = 100;
        //LevelManager.Player1MaxAmmo = 10;

        // TODO: REMOVE THESE - FAKE DATA
        //LevelManager.Player2Parts = 1;
        //LevelManager.Player2Powerups = 2;
        //LevelManager.Player2Win = !LevelManager.Player1Win;
        //LevelManager.Player2Life = 9;
        //LevelManager.Player2MaxHealth = 100;
        //LevelManager.Player2MaxAmmo = 10;

        // Keep these
        if (shipType == EnumShipType.Player1)
        {
            PartScore = LevelManager.Player1Parts * 1000;
            PowerUps = LevelManager.Player1Powerups;
            PowerUpScore = LevelManager.Player1Powerups * 1000;
            PlayerLife = LevelManager.Player1Life;
            PlayerParts = LevelManager.Player1Parts;
            PlayerBonus = LevelManager.Player1Win ? 10000 : 0;
            PlayerTotal = LevelManager.Player1Score;
            PlayerMaxHealth = LevelManager.Player1MaxHealth;
            PlayerMaxAmmo = LevelManager.Player1MaxAmmo;
        }
        else
        {
            PartScore = LevelManager.Player2Parts * 1000;
            PowerUps = LevelManager.Player2Powerups;
            PowerUpScore = LevelManager.Player2Powerups * 1000;
            PlayerLife = LevelManager.Player2Life;
            PlayerParts = LevelManager.Player2Parts;
            PlayerBonus = LevelManager.Player2Win ? 10000 : 0;
            PlayerTotal = LevelManager.Player2Score;
            PlayerMaxHealth = LevelManager.Player2MaxHealth;
            PlayerMaxAmmo = LevelManager.Player2MaxAmmo;
        }

        LevelScore = PartScore + PowerUpScore + PlayerBonus;

        PartsText.text = $"{PlayerParts} x 1000 = {PartScore}";
        PowerUpsText.text = $"{PowerUps} x 1000 = {PowerUpScore}";
        PlayerPartsText.text = $"{PlayerParts}";
        PlayerLifeText.text = $"{PlayerLife}";
        LevelText.text = $"{LevelScore}";
        TotalCountText.text = $"{PlayerTotal + LevelScore}";
        PlayerMaxHealthText.text = $"{PlayerMaxHealth}";
        PlayerMaxAmmoText.text = $"{PlayerMaxAmmo}";
        WinBonusText.text = $"{PlayerBonus} pts";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalculatePlayerScore()
    {
        if(shipType == EnumShipType.Player1)
        {
            LevelManager.Player1Score = LevelManager.Player1Score + LevelScore;
        }
        else
        {
            LevelManager.Player2Score = LevelManager.Player2Score + LevelScore;
        }

        LevelManager.LoadNext();
    }

    public void IncreaseMaxHealth()
    {
        if (PlayerParts <= 0) return;

        PlayerPartsText.text = $"{--PlayerParts}";
        PlayerMaxHealthText.text = $"{PlayerMaxHealth += 25}";
        
        if(shipType == EnumShipType.Player1)
        {
            LevelManager.Player1Parts = PlayerParts;
            LevelManager.Player1MaxHealth = PlayerMaxHealth;
        }
        else
        {
            LevelManager.Player2Parts = PlayerParts;
            LevelManager.Player2MaxHealth = PlayerMaxHealth;
        }
    }

    public void IncreaseMaxAmmo()
    {
        if (PlayerParts <= 0) return;

        PlayerPartsText.text = $"{--PlayerParts}";
        PlayerMaxAmmoText.text = $"{++PlayerMaxAmmo}";

        if (shipType == EnumShipType.Player1)
        {
            LevelManager.Player1Parts = PlayerParts;
            LevelManager.Player1MaxAmmo = PlayerMaxAmmo;
        }
        else
        {
            LevelManager.Player2Parts = PlayerParts;
            LevelManager.Player2MaxAmmo = PlayerMaxAmmo;
        }
    }

    public void IncreaseMaxLife()
    {
        if (PlayerParts <= 0) return;

        PlayerPartsText.text = $"{--PlayerParts}";
        PlayerLifeText.text = $"{++PlayerLife}";

        if (shipType == EnumShipType.Player1)
        {
            LevelManager.Player1Parts = PlayerParts;
            LevelManager.Player1Life = PlayerLife;
        }
        else
        {
            LevelManager.Player2Parts = PlayerParts;
            LevelManager.Player2Life = PlayerLife;
        }
    }
}
