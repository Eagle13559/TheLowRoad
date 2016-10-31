﻿using UnityEngine;
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
        Application.LoadLevel(1);
    }

    public void restartLevel()
    {
        player.PlayerRespawn();
    }

    public void exitLevel()
    {
        Application.LoadLevel(0);
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