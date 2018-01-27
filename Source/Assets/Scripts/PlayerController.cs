using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public Sprite spJumpSprite;
    public Sprite spEvadeSprite;
    public Sprite spIdleSprite;
    public Sprite spMoveLeft;
    public Sprite spMoveRight;

    bool _isTransmitting = false;
    GameObject ExecutionList;
    // Use this for initialization
    void Start () {
        ExecutionList = GameObject.Find("ExecutionList");
        if(transform.position.x > 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (!isLocalPlayer)
        {
            SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
            renderer.color = new Color32(184, 255, 88, 255);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(GameManager.GameState == "Transmission" && !_isTransmitting)
        {
            _isTransmitting = true;
            TrasmitActions();
        }
    }

    void TrasmitActions()
    {
        Debug.Log("Executing Actions");

        //Add Transmissions To List
        foreach (Transform child in ExecutionList.transform)
        {
            if (child.gameObject.GetComponent<Text>().text != "Action Chain:")
                GameManager.ActionList.Add(child.gameObject.GetComponent<Text>().text);
        }

        GameManager.GameState = "Transmission";

        StartCoroutine(executeActions(GameManager.ActionList));
    }

    IEnumerator executeActions(List<string> ActionList)
    {
        //Execute Actions
        foreach (string action in ActionList)
        {
            switch (action)
            {
                case "MoveRightCard":
                    MoveRight();
                    break;
                case "MoveLeftCard":
                    MoveLeft();
                    break;
                case "JumpCard":
                    Jump();
                    yield return new WaitForSeconds(GameManager.executionTime);
                    Fall();
                    break;
                case "EvadeCard":
                    Evade();
                    break;
            }
            //Delay Actinos 
            yield return new WaitForSeconds(GameManager.executionTime);
        }

        GameManager.GameState = "Planning";
        _isTransmitting = false;
        ActionList.Clear();
    }


    public void MoveLeft()
    {
        if (!isLocalPlayer) return;

        transform.Translate(new Vector3(-1, 0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveLeft;
    }

    public void MoveRight()
    {
        if (!isLocalPlayer) return;

        transform.Translate(new Vector3(1, 0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveRight;

    }

    public void Jump()
    {
        if (!isLocalPlayer) return;

        transform.Translate(new Vector3(0, 1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spJumpSprite;
    }

    public void Evade()
    {
        if (!isLocalPlayer) return;

        gameObject.GetComponent<SpriteRenderer>().sprite = spEvadeSprite;
    }

    public void Fall()
    {
        if (!isLocalPlayer) return;

        transform.Translate(new Vector3(0, -1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spIdleSprite;
    }
}
