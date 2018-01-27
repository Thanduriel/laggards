using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : GameState
{
    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

        //yield break;
    }
    public override IEnumerator GameStateUpdate()
    {
        yield break;
    }
}
