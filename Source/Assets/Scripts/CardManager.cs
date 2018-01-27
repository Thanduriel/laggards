﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    //Attributes
    public GameObject[] CardList;
    bool _isGenerated = false;
    // Use this for initialization
    void Start()
    {
        //Spawn Basic Deck
        GenerateDeck();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameState == "Transmission" && _isGenerated)
        {
            clearDeck();
        }

        if (GameManager.GameState == "Planning" && transform.childCount == 0)
        {
            if (!_isGenerated)
                GenerateDeck();
        }

        Debug.Log(_isGenerated);
    }

    public void clearDeck()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        _isGenerated = false;
    }

    private void GenerateDeck()
    {
        for (int i = 1; i <= GameManager.cardLimit; i++)
        {
            int index = Random.Range(1, CardList.Length);
            Instantiate(CardList[index], transform);
            _isGenerated = true;
        }
    }
}
