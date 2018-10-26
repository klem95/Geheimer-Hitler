using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField]
	static public List <Player> players;

	static public bool all_players_are_ready;
	int locked_in_players;



	void Awake() {
        DontDestroyOnLoad (transform.gameObject);
    }

	// Use this for initialization
	void Start () {

		players = new List<Player> ();
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}


	// Game size selection and player registration  \\

	public void initialize_game_size (int _people) {

		for (int i = 0; i < _people; i ++) {
			players.Add (new Player (i));
		}

		
		SceneManager.LoadScene ("Player_registration");


	}


	public void player_registration (int ID, string _input_string) {

		players[ID].user_name = _input_string;
		Debug.Log ("The player with ID: " + ID + " has chosen the name: " + players[ID].user_name);
		

	}

	public void current_lock_status (int ID) {
		if (!players[ID].locked_in) {
			players[ID].locked_in = true;
			locked_in_players++;

		} else {
			players[ID].locked_in = false;
			locked_in_players--;
		}

		if (locked_in_players == players.Count) {
				all_players_are_ready = true;
			} else {
				all_players_are_ready = false;
			}

		Debug.Log ("The player with ID: " + ID + " has lock status: " + players[ID].locked_in);
		
	}







}
