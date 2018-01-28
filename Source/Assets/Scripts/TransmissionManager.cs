using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionManager : MonoBehaviour
{

    GameObject ExecutionList;
    bool _enableTransmission = false;

    // Use this for initialization
    void Start()
    {
        ExecutionList = GameObject.Find("ExecutionList");
        //Add Button Event
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { if (_enableTransmission) { TrasmitActions(); } });
    }

    // Update is called once per frame
    void Update()
    {
        if (ExecutionList.transform.childCount > 1)
        {
            _enableTransmission = true;
        }
        else
        {
            _enableTransmission = false;
        }

        if (_enableTransmission)
        {
            gameObject.transform.Find("Text").GetComponent<Text>().color = Color.red;
        }
        else
        {
            gameObject.transform.Find("Text").GetComponent<Text>().color = Color.gray;
        }
    }

    void TrasmitActions()
    {
        GameManager.GameState = "Transmission";
    }
}
