using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformAction : GameState
{
    private GameObject player;
    public GameObject notificationTransmission;
    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

        player = GameObject.FindGameObjectWithTag("Player");

        GameObject exList = gSC.executionList;

        StartCoroutine(ExecuteActions());

        DestroyChildren(exList.transform);

        notificationTransmission.SetActive(true);


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
        notificationTransmission.SetActive(false);
        yield return gSC.SwitchGameState(gSC.actionSelection);
    }
}
