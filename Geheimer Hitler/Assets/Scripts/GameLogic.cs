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
	static public byte locked_in_players;

	static public string [] turn_tracker;

	static public string game_size;

	static public int played_facist_cards;
	static public int played_liberal_cards;

	static public int [] power_up_pos;

	public GameObject [] small_b;
	public GameObject [] medium_b;

	public GameObject [] large_b;


	public Sprite locked_in;

	bool initializing_game;

	public static Board board_layout;


	static public int facist_card_played;
	static public int liberal_card_played;

	public static int election_tracker_counter;
	


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

		initializing_game = false;

		
	}
	
	// Game size selection and player registration  \\

	public void initialize_game_size (int _people) {

		if (!initializing_game) {
			initializing_game = true;
			turn_tracker = new string [_people];

			for (byte i = 0; i < _people; i ++) {
				players.Add (new Player (i));
			}

			if (_people <= 6) {

				power_up_pos = new int [3];

				for (int i = 3; i < power_up_pos.Length+3; i++) {
					power_up_pos[i-3] = i;
				}

				small_b[_people - 5].GetComponent<Image>().sprite = locked_in;
				small_b[_people - 5].GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition -= new Vector3 (0,15,0);
				
				game_size = "small";

				board_layout = new Board (game_size);

				Debug.Log ("the board to set up: " + board_layout.power_ups_layout[2]);

			} else if (_people <= 8) {
				medium_b[_people - 7].GetComponent<Image>().sprite = locked_in;
				medium_b[_people - 7].GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition -= new Vector3 (0,15,0);
				game_size = "medium";
				board_layout = new Board (game_size);
			} else {
				large_b[_people - 9].GetComponent<Image>().sprite = locked_in;
				large_b[_people - 9].GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition -= new Vector3 (0,15,0);
				game_size = "large";
				board_layout = new Board (game_size);
			}
			
			StartCoroutine ("scene_transition");
		}

	}

	IEnumerator scene_transition () {
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("Player_registration");
	}

	public void player_registration (int ID, string _input_string) {

		turn_tracker [ID] = _input_string;
		players[ID].user_name = _input_string;
		//Debug.Log ("The player with ID: " + ID + " has chosen the name: " + players[ID].user_name);
		

	}
	public void current_lock_status () {
		// if (!players[ID].locked_in) {
		// 	players[ID].locked_in = true;
		// 	locked_in_players++;

		// } else {
		// 	players[ID].locked_in = false;
		// 	locked_in_players--;
		// }

		//Debug.Log("********************");

		int count = 0;

		foreach (Player obj in players)
		{
			if (obj.locked_in) {
				count++;
			}

			//Debug.Log("User: " + obj.user_name + "´s looked in status is currently: " + obj.locked_in);
		}

		if (players.Count == count) 
		{
				all_players_are_ready = true;
		} else {
				all_players_are_ready = false;
		}





		// if (locked_in_players == players.Count) {
		// 		all_players_are_ready = true;
		// 	} else {
		// 		all_players_are_ready = false;
		// 	}

		//Debug.Log ("The player with ID: " + ID + " has lock status: " + players[ID].locked_in);
		
	}

	static public void election_tracker () {

		elections_failed++;

		if (elections_failed == 4) {
			Debug.Log("Election failed, Show top card in deck");
			elections_failed = 0;
		}
	}

	public void temp_load_func () {
		for (byte i = 0; i < 5; i ++) {
				players.Add (new Player (i));
				players[i].ID = i;
				players[i].user_name = i.ToString();

				game_size = "small";

				board_layout = new Board (game_size);

				SceneManager.LoadScene ("Role_delegation");
			}
	}
}
