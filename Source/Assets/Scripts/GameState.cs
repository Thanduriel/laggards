using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private GameStateController gSC;

    public virtual IEnumerator Init(GameStateController gSC)
    {
        this.gSC = gSC;
        yield break;
    }
    public virtual IEnumerator Update()
    {
        yield break;
    }
}

public class SetupGame : GameState
{
    public override IEnumerator Init(GameStateController gSC)
    {
        base.Init(gSC);
        yield break;
    }
}

public class ActionSelection : GameState
{

}

public class PerformAction : GameState
{

}

public class PauseMenu : GameState
{

}