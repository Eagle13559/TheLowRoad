using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public GameObject instructionsPanel;
    public GameObject narrativePanel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startLevel()
    {
        Application.LoadLevel(0);
    }

    public void restartLevel()
    {
        player.PlayerRespawn();
    }

    public void exitLevel()
    {
        Application.LoadLevel(4);
    }

    public void nextLevel()
    {
        int currentLevel = Application.loadedLevel;
        Application.LoadLevel(++currentLevel);
    }

    public void jumpToLevel(int level_)
    {
        Application.LoadLevel(level_);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void showInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void hideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void showNarrative()
    {
        narrativePanel.SetActive(true);
    }

    public void hideNarrative()
    {
        narrativePanel.SetActive(false);
    }
}
