using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameState startGamestate;
    [HideInInspector] public GameState currentGamestate;

    private bool isSwitching;

    // Use this for initialization
    private void Start()
    {
        currentGamestate = startGamestate;
        currentGamestate.Init(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if ((!isSwitching))
        {
            currentGamestate.Update();
        }
    }

    public IEnumerator SwitchGameState(GameState newGameState)
    {
        isSwitching = true;

        currentGamestate = newGameState;
        yield return StartCoroutine(currentGamestate.Init(this));

        isSwitching = false;
    }
}
