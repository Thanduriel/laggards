using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Sprite spJumpSprite;
    public Sprite spEvadeSprite;
    public Sprite spIdleSprite;
    public Sprite spMoveLeft;
    public Sprite spMoveRight;
	// Use this for initialization
	void Start () {
        //Position Player
        this.transform.position = new Vector3(-4.21f, -2.63f, -2);
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
    }

    public void MoveLeft()
    {
        transform.Translate(new Vector3(-1, 0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveLeft;
    }

    public void MoveRight()
    {
        transform.Translate(new Vector3(1, 0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveRight;

    }

    public void Jump()
    {
        transform.Translate(new Vector3(0, 1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spJumpSprite;
    }

    public void Evade()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spEvadeSprite;
    }

    public void Fall()
    {
        transform.Translate(new Vector3(0, -1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spIdleSprite;
    }
}
