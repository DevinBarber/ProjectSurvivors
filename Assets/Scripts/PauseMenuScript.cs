using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    private GameManagerScript gameManagerScript;

    private void Start()
    {
        gameManagerScript = GetComponent<GameManagerScript>();
    }

    public void ResumeGame()
    {
        gameManagerScript.UnpauseGame();
    }

    public void OpenOptions()
    {
        //Open options
    }
}
