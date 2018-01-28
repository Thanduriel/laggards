using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PerformAction : GameState
{
    private GameObject playerHandler;
    public GameObject notificationTransmission;
    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in playersList)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                playerHandler = player.gameObject;
            }
        }
        
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

		PlayerController playerController = playerHandler.GetComponent<PlayerController>();
		if (playerController.isLocalPlayer)
			foreach (string action in ActionList)
			{

				switch (action)
				{
					case "MoveRightCard":
						playerController.MoveRight();
						break;
					case "MoveLeftCard":
						playerController.MoveLeft();
						break;
					case "JumpCard":
						playerController.Jump();
						yield return new WaitForSeconds(GameManager.executionTime * 0.2f);
						playerController.MoveJump();
						yield return new WaitForSeconds(GameManager.executionTime*0.4f);
						playerController.Idle();
						break;
					case "EvadeCard":
						playerController.Evade();
						break;
					case "StrikeForwardCard":
						yield return new WaitForSeconds(GameManager.executionTime * 0.25f);
						playerController.StrikeForward();
						break;
				}
				//Delay actions 
				yield return new WaitForSeconds(GameManager.executionTime);

				// return to idle state
				playerHandler.GetComponent<PlayerController>().Idle();
			}
        notificationTransmission.SetActive(false);
        yield return gSC.SwitchGameState(gSC.actionSelection);
    }
}
