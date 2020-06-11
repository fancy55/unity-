using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
	public int lifeV = 3;
	public int playerScore = 0;
	public bool isDead;
	public bool isDefeat;

	public GameObject born;
	public Text playerScoreText;
	public Text PlayerLifeValueText;
	public GameObject isDefeatUI;

	private static PlayerManager instance;

	public static PlayerManager Instance{
		get{
			return instance;
		}
		set{
			instance = value;
		}
	}


	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDefeat) {
			isDefeatUI.SetActive (true);
			Invoke ("back",3);
			return;
		}
		if (isDead) {
			Recover ();
		}
		playerScoreText.text = playerScore.ToString ();
		PlayerLifeValueText.text = lifeV.ToString ();
	}

	private void Recover(){
		if (lifeV <= 0) {
			isDead = true;
			Invoke ("back", 3);
		} else {
			lifeV--;
			GameObject go = Instantiate (born, new Vector3 (-2, -8, 0),Quaternion.identity);
			go.GetComponent<born> ().createPlayer = true;
			isDead = false;
		}
	}

	private void back(){
		SceneManager.LoadScene (0);
	}
}
