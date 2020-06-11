using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	//属性
	public float speed = 3;
	private Vector3 bullectEA;
	private float timeval;
	private float deTimeval;
	private bool isDefend = true;
	//引用
	private SpriteRenderer sr;
	public Sprite[] tank;
	public GameObject bullect;
	public GameObject explosion;
	public GameObject defendEffect;
	public AudioSource moveAudio;
	public AudioClip[] tankAudio;
	// Use this for initialization

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(isDefend){
			defendEffect.SetActive (true);
			deTimeval -= Time.deltaTime;
			if (deTimeval <= 0) {
				isDefend = false;
				defendEffect.SetActive (false);
			}
		}
	}

	private void FixedUpdate(){ //固定物理帧
		if(PlayerManager.Instance.isDefeat)return;
		Move();
		if (timeval >= 0.4f) {
			Attack ();
		} else
			timeval += Time.deltaTime;
	}


	private void Attack(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Instantiate(bullect,transform.position,Quaternion.Euler(transform.eulerAngles+bullectEA));
			timeval = 0;
		}
	}

	private void Move(){
		float h = Input.GetAxisRaw ("Horizontal");
		transform.Translate (Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);
		if (h < 0) { 
			sr.sprite = tank [3];
			bullectEA = new Vector3 (0, 0, 90);
		} else if (h > 0) {
			sr.sprite = tank [1];
			bullectEA = new Vector3 (0, 0, -90);
		}
		if (h != 0)//优先级
			return;
		float v = Input.GetAxisRaw ("Vertical");
		transform.Translate (Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
		if (v < 0) { 
			sr.sprite = tank [2];
			bullectEA = new Vector3 (0, 0, -180);
		} else if (v > 0) {
			sr.sprite = tank [0];
			bullectEA = new Vector3 (0, 0, 0);
		}

		if (Mathf.Abs (h) > 0.05f) {
			moveAudio.clip = tankAudio [1];
			if (!moveAudio.isPlaying)
				moveAudio.Play ();
		} else {
			moveAudio.clip = tankAudio [0];
			if (!moveAudio.isPlaying)
				moveAudio.Play ();
		}
		
	}

	private void Die(){
		if (isDefend)
			return;
		PlayerManager.Instance.isDead = true;
		Instantiate (explosion, transform.position,transform.rotation);
		Destroy (gameObject);
	}
}
