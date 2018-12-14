using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField]
	static public List <Player> players;

	static public List <Log_data> logged_data;

	static public byte elections_failed;

	static public bool all_players_are_ready;
	byte locked_in_players;

	static public string [] turn_tracker;

	static public string game_size;

	static public int played_facist_cards;
	static public int played_liberal_cards;

	static public int [] power_up_pos;



	void Awake() {
        DontDestroyOnLoad (transform.gameObject);
    }

	// Use this for initialization
	void Start () {
		played_facist_cards = 0;
		played_liberal_cards = 0;
		players = new List<Player> ();
		logged_data = new List <Log_data> ();
		elections_failed = 0;
	}
	
	// Game size selection and player registration  \\

	public void initialize_game_size (int _people) {

		turn_tracker = new string [_people];

		for (byte i = 0; i < _people; i ++) {
			players.Add (new Player (i));
		}

		if (_people <= 6) {

			power_up_pos = new int [3];

			for (int i = 3; i < power_up_pos.Length+3; i++) {
				power_up_pos[i-3] = i;
			}
			
			game_size = "small";
			Debug.Log ("Game size small");

		} else if (_people <= 8) {
			game_size = "medium";
		} else {
			game_size = "marge";
		}
		
		SceneManager.LoadScene ("Player_registration");

	}
	public void player_registration (int ID, string _input_string) {

		turn_tracker [ID] = _input_string;
		players[ID].user_name = _input_string;
		//Debug.Log ("The player with ID: " + ID + " has chosen the name: " + players[ID].user_name);
		

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

		//Debug.Log ("The player with ID: " + ID + " has lock status: " + players[ID].locked_in);
		
	}

	static public void election_tracker () {

		elections_failed++;

		if (elections_failed == 4) {
			Debug.Log("Election failed, Show top card in deck");
			elections_failed = 0;
		}
	}
}
