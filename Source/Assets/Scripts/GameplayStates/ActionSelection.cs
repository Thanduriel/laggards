using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelection : GameState
{
    //Attributes
    public GameObject[] CardList;
	public int[] CardProbabilities;
	public GameObject doActionButton;
    bool enableTransmission = false;

    public override void Init(GameStateController gSC)
    {
        base.Init(gSC);

		for (int i = 0; i < CardProbabilities.Length; ++i)
			for (int j = 0; j < CardProbabilities[i]; ++j)
				WeightedCards.Add(CardList[i]);

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
        int Capacity = PlayerController.cardLimit - gSC.cardWidget.transform.childCount;
        for (int i=1;i<= Capacity; i++)
        {
            Instantiate(WeightedCards[Random.Range(0, WeightedCards.Count)], gSC.cardWidget.transform);
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

	private List<GameObject> WeightedCards = new List<GameObject>();
}
