using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameState startGamestate;

    public SetupGame setupGame;
    public ActionSelection actionSelection;
    public PerformAction performAction;
    public PauseMenu pauseMenu;
    public EndGame endGame;

    [HideInInspector] public GameState currentGamestate;
    public GameObject executionList;

    private bool isSwitching = false;

    // Use this for initialization
    private void Start()
    {
        executionList = GameObject.Find("ExecutionList");
    }

    // Update is called once per frame
    private void Update()
    {
        if(currentGamestate == null)
        {
            currentGamestate = startGamestate;
            currentGamestate.Init(this);
        }

        if (!isSwitching)
        {
            StartCoroutine(currentGamestate.GameStateUpdate());
        }
    }

    public IEnumerator SwitchGameState(GameState newGameState)
    {
        isSwitching = true;

        currentGamestate = newGameState;
        currentGamestate.Init(this);

        isSwitching = false;

        yield break;
    }
}
