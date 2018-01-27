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

		// smooth movement
		float nextMovement = Mathf.Max(0f, m_bufferedMovement - Time.deltaTime);
		float move = m_bufferedMovement - nextMovement;
		m_bufferedMovement = nextMovement;
       // transform.Rotate(0, x, 0);
        if(move != 0f) transform.Translate(m_movementDir * move, 0, 0);
    }

    public void MoveLeft()
    {
		m_movementDir = -1f;
		m_bufferedMovement = 1f;
	   // transform.Translate(new Vector3(-1, 0, 0));
		gameObject.GetComponent<SpriteRenderer>().sprite = spMoveLeft;
    }

    public void MoveRight()
    {
		m_movementDir = 1f;
		m_bufferedMovement = 1f;
	//	transform.Translate(new Vector3(1, 0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveRight;

    }

    public void Jump()
    {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
     //   transform.Translate(new Vector3(0, 1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spJumpSprite;
    }

    public void Evade()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spEvadeSprite;
    }

    public void Fall()
    {
      //  transform.Translate(new Vector3(0, -1, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = spIdleSprite;
    }

	private float m_bufferedMovement;
	private float m_movementDir;
}
