using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public string direction;
    int speed = 3;
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(direction == "left")
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.right * speed * Time.deltaTime);

    }
}
