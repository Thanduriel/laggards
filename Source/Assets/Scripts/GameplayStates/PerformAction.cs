using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformAction : GameState
{
    private GameObject player;

    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

        player = GameObject.FindGameObjectWithTag("Player");

        GameObject exList = gSC.executionList;
        if (gSC.executionList.transform.childCount > 1)
        {
            DestroyChildren(exList.transform, null, "ChainText(Clone)");
        }

        StartCoroutine(ExecuteActions());

        DestroyChildren(exList.transform);
        //yield break;
    }

    public override IEnumerator GameStateUpdate()
    {
        yield break;
    }

    IEnumerator ExecuteActions()
    {
        //Create Transmission Array
        List<string> ActionList = new List<string>();

        //Add Transmissions To List
        foreach (Transform child in gSC.executionList.transform)
        {
            if (child.name == "ChainText(Clone)")
            {
                ActionList.Add(child.gameObject.GetComponent<Text>().text);
            }
        }
        //foreach (Text child in gSC.executionList.transform.GetComponentsInChildren<Text>())
        //{
        //    ActionList.Add(child.text);
        //    //ActionList.Add(child.gameObject.GetComponent<Text>().text);
        //}

        //Execute Actions
        foreach (string action in ActionList)
        {
            switch (action)
            {
                case "MoveRightCard":
                    player.GetComponent<PlayerController>().MoveRight();
                    break;
                case "MoveLeftCard":
                    player.GetComponent<PlayerController>().MoveLeft();
                    break;
                case "JumpCard":
                    player.GetComponent<PlayerController>().Jump();
                    yield return new WaitForSeconds(GameManager.executionTime);
                    player.GetComponent<PlayerController>().Fall();
                    break;
                case "EvadeCard":
                    player.GetComponent<PlayerController>().Evade();
                    break;
            }
            //Delay Actinos 
            yield return new WaitForSeconds(GameManager.executionTime);
        }

        yield return gSC.SwitchGameState(gSC.actionSelection);
    }
}
