using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    public List<Image> Player1Ships;
    public List<Image> Player2Ships;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlayer1Ship(int index)
    {
        LevelManager.Player1Ship = index;
        foreach(var image in Player1Ships)
        {
            Color c = image.color;
            c.a = 0;
            image.color = c;
        }
        Color p1c = Player1Ships[index].color;
        p1c.a = 1;
        Player1Ships[index].color = p1c;

        LevelManager.Player1Ship = index;
        print($"SelectPlayer1Ship: {LevelManager.Player1Ship}");
    }

    public void SelectPlayer2Ship(int index)
    {
        LevelManager.Player2Ship = index;
        foreach(var image in Player2Ships)
        {
            Color c = image.color;
            c.a = 0;
            image.color = c;
        }        
        Color p2c = Player2Ships[index].color;
        p2c.a = 1;
        Player2Ships[index].color = p2c;

        LevelManager.Player2Ship = index;
        print($"SelectPlayer2Ship: {LevelManager.Player2Ship}");
    }    

}

