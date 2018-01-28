using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { addAction(gameObject.name.Replace("(Clone)","")); });
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void addAction(string actionDescription)
    {
        GameObject TextPrefab;
        TextPrefab = (GameObject) Instantiate(GameObject.Find("ChainText"), GameObject.Find("ExecutionList").transform);
        TextPrefab.GetComponent<Text>().text = actionDescription;
        Destroy(gameObject);
    }


}
