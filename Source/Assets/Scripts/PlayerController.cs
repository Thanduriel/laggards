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
        if (transform.position.x > 0)
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
        if (!isLocalPlayer){return;}

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		// smooth movement
		float nextMovement = Mathf.Max(0f, m_bufferedMovement - Time.deltaTime);
		float move = m_bufferedMovement - nextMovement;
		m_bufferedMovement = nextMovement;
        if(move != 0f) transform.Translate(m_movementDir * move, 0, 0);
    }

    public void MoveLeft()
    {
        if (!isLocalPlayer) { return; }
        m_movementDir = -1f;
		m_bufferedMovement = 1f;
		gameObject.GetComponent<SpriteRenderer>().sprite = spMoveLeft;
    }

    public void MoveRight()
    {
        if (!isLocalPlayer) { return; }
        m_movementDir = 1f;
		m_bufferedMovement = 1f;
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveRight;

    }

    public void Jump()
    {
        if (!isLocalPlayer) { return; }
        Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
        gameObject.GetComponent<SpriteRenderer>().sprite = spJumpSprite;
    }

    public void Evade()
    {
        if (!isLocalPlayer) { return; }
        gameObject.GetComponent<SpriteRenderer>().sprite = spEvadeSprite;
    }

    public void Fall()
    {
        if (!isLocalPlayer) { return; }
        gameObject.GetComponent<SpriteRenderer>().sprite = spIdleSprite;
    }

	private float m_bufferedMovement;
	private float m_movementDir;
}
