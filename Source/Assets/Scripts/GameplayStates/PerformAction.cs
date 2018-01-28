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
						playerController.MoveRight(1f);
						break;
					case "MoveLeftCard":
						playerController.MoveLeft(1f);
						break;
					case "MoveRight2":
						playerController.MoveRight(2f);
						break;
					case "MoveLeft2":
						playerController.MoveLeft(2f);
						break;
					case "MoveRight3":
						playerController.MoveRight(3f);
						break;
					case "MoveLeft3":
						playerController.MoveLeft(3f);
						break;
					case "JumpCard":
						playerController.Jump(8f);
						yield return new WaitForSeconds(GameManager.executionTime * 0.2f);
						playerController.Move(1f, PlayerController.MovementDir.Current);
						yield return new WaitForSeconds(GameManager.executionTime*0.4f);
						playerController.Idle();
						break;
					case "StrongJumpCard":
						playerController.Jump(10f);
						yield return new WaitForSeconds(GameManager.executionTime * 0.2f);
						playerController.Move(2f, PlayerController.MovementDir.Current);
						yield return new WaitForSeconds(GameManager.executionTime * 0.4f);
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
