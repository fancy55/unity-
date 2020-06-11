using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullect : MonoBehaviour {
	public float speed = 10;
	public bool isBullect = true;//玩家子弹true
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision){
		switch (collision.tag) {
			case "tank":
				if (!isBullect) {
					collision.SendMessage ("Die"); 
					Destroy (gameObject);
				}
				break;
			case "heart":
				collision.SendMessage("Die"); 
				Destroy (gameObject);
				break;
			case "enermy":
				if (isBullect) {
					collision.SendMessage ("Die"); 
					Destroy (gameObject);
				}
				break;
			case "wall":
				Destroy (collision.gameObject);
				Destroy (gameObject);
				break;
			case "barrier":
				if(isBullect)
					collision.SendMessage ("PlayAudio"); 
				Destroy (gameObject);
				break;
			default:
				break;
		}
	}
}