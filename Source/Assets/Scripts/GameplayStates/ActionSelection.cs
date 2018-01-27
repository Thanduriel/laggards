using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelection : GameState
{
    //Attributes
    public GameObject[] CardList;
    public GameObject doActionButton;
    bool enableTransmission = false;

    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

        ClearDeck();
        GenerateDeck();

    }

    public override IEnumerator GameStateUpdate()
    {
        if (gSC.executionList.transform.childCount > 1)
        {
            enableTransmission = true;
        }
        else
        {
            enableTransmission = false;
        }

        if (enableTransmission)
        {
            GameObject.Find("Text").GetComponent<Text>().color = Color.red;
        }
        else
        {
            GameObject.Find("Text").GetComponent<Text>().color = Color.gray;
        }
        yield break;
    }

    public void ClearDeck()
    {
        DestroyChildren(this.transform);
    }

    private void GenerateDeck()
    {
        foreach (GameObject card in CardList)
        {
            Instantiate(card, gSC.cardWidget.transform);
        }
    }

    public void TransmitActions()
    {
        if (enableTransmission)
        {
            Debug.Log("Transmitting Actions");

            StartCoroutine(gSC.SwitchGameState(gSC.performAction));
        }
    }
}
