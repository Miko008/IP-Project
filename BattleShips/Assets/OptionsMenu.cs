using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void ChoosePlayingMode(bool isPlayerChecked)
    {
        if (isPlayerChecked)
        {
            // grasz przeciwko graczowi
            Debug.Log("You play against another player!");
        }
        else
        {
            // grasz przeciwko AI - trzeba ustawiæ poziom trudnoœci
            Debug.Log("You play against AI!");
        }
        
    }

    public void SetAIDifficulty(int difficulty)
    {
        // ustawia trudnoœæ AI
        Debug.Log("Difficulty of AI is changed");
    }

    public void SetGameDifficulty(int difficulty)
    {
        // ustawia trudnoœæ AI
        Debug.Log("Game difficulty is changed");
    }

    public void ChangeMapSize(int mapSizeIndex)
    {
        // ustawia trudnoœæ AI
        Debug.Log("Map size is changed");
    }
}
