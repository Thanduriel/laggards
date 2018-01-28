using System;
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
	public Sprite spStrikeForward;

	public Vector2 evadeHitBoxSize;
	public Vector2 evadeHitBoxOffset;

	// Use this for initialization
    public static int cardLimit = 4;
	// Use this for initialization
	void Start () {
		SetMovementDir(transform.position.x > 0f ? MovementDir.Left : MovementDir.Right);

		// fetch default size
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		hitBoxSize = collider.size;
		hitBoxOffset = collider.offset;

		if (!isLocalPlayer)
        {
            SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
            renderer.color = new Color32(184, 255, 88, 255);
        }
    }
	
	// Update is called once per frame
	void Update () {
		// smooth movement
		float nextMovement = Mathf.Max(0f, m_bufferedMovement - Time.deltaTime);
		float move = m_bufferedMovement - nextMovement;
		m_bufferedMovement = nextMovement;
        if(move != 0f) transform.Translate(m_movementDir * move, 0, 0);
    }

    public void MoveLeft()
    {
		SetMovementDir(MovementDir.Left);
		m_bufferedMovement = 1f;
		gameObject.GetComponent<SpriteRenderer>().sprite = spMoveLeft;
    }


    public void MoveRight()
    {
		SetMovementDir(MovementDir.Right);
		m_bufferedMovement = 1f;
        gameObject.GetComponent<SpriteRenderer>().sprite = spMoveRight;
    }

    public void Jump()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
        gameObject.GetComponent<SpriteRenderer>().sprite = spJumpSprite;
    }
	public void MoveJump()
	{
		m_bufferedMovement = 1f;
	}

    public void Evade()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spEvadeSprite;
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		collider.size = evadeHitBoxSize;
		collider.offset = evadeHitBoxOffset;
	}

    public void Idle()
    {
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		spriteRenderer.sprite = spIdleSprite;

		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		collider.size = hitBoxSize;
		collider.offset = hitBoxOffset;
	}

	public void StrikeForward()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = spStrikeForward;

		RaycastHit2D[] hitInfos = new RaycastHit2D[2];
		int hits = Physics2D.Raycast(transform.position, new Vector3(m_movementDir,0f,0f), 
			new ContactFilter2D(), hitInfos, 2f);

		PlayerController oth;
		if (hits > 1 && (oth = hitInfos[1].collider.gameObject.GetComponent<PlayerController>()))
			Destroy(hitInfos[1].collider.gameObject);
		// todo deal damage here
	}

	enum MovementDir
	{
		Right,
		Left
	}
	private void SetMovementDir(MovementDir dir)
	{
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		if(dir == MovementDir.Left){
			spriteRenderer.flipX = true;
			m_movementDir = -1f;
		}
		else{
			spriteRenderer.flipX = false;
			m_movementDir = 1f;
		}
	}
	
	private float m_bufferedMovement;
	private float m_movementDir;

	private Vector2 hitBoxSize;
	private Vector2 hitBoxOffset;
}
