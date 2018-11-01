using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Role_delegation : MonoBehaviour {

	string [,] potical_profile = {
		{"Facist","Adolf Hitler"},
		{"Facist","Joseph Goebbels"},
		{"Facist","Heinrich Himmler"},
		{"Facist","Joseph Mengele"},
		{"Liberal","Winston Churchill"},
		{"Liberal","Franklin D. Roosevelt"},
		{"Liberal","Charles de Gaulle"},
		{"Liberal","Dwight Eisenhower"},
		{"Liberal","Neville Chamberlain"},
		{"Liberal","William King"},
		};

		List <string> political_profiles;		
	Random random;

	[SerializeField]
	TMP_Text player_name_display, player_role_display;

	byte player_no_2_displayer;



	void Awake () {

		player_no_2_displayer = 0;

		political_profiles = delegationg_roles ();
		assign_roles(political_profiles);

		player_name_display.text = GameLogic.players[player_no_2_displayer].user_name;
		
		// foreach (Player obj in GameLogic.players) {
		// 	Debug.Log ("Player: " + obj.user_name + " playes as: " + obj.membership_card + " with the role: " + obj.profile_card);
		// }

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	List <string> delegationg_roles () {
		List <string> temp_list = new List<string>();

		short lim,offset = 0; /// maybe this can have **** things up 

		if (GameLogic.game_size == "small") {
			lim = 2;
			offset = 2;
		} else if (GameLogic.game_size == "medium") {
			lim = 2;
			offset = 1;
		} else {
			lim = 4;
		}

			for (byte i = 0; i < GameLogic.players.Count; i++) {
				if (i < lim) { 
					temp_list.Add(potical_profile[i,0] + "-" + potical_profile[i,1]);
				} else {
					temp_list.Add(potical_profile[i+offset,0] + "-" + potical_profile[i+offset,1]);
				}

			}
		

		return shuffle_list(temp_list);

	}

	List <string> shuffle_list (List <string> role_list) {
		List <string> temp_list = new List<string>();
		byte rnd_no;

		while (role_list.Count > 0) {
			rnd_no = (byte)Random.Range (0,role_list.Count);
			temp_list.Add(role_list[rnd_no]);
			role_list.RemoveAt (rnd_no);
		}

		return temp_list;
	}

	public void assign_roles (List <string> roles) {
		byte count = 0;
		string [] temp_string;

		foreach (Player obj in GameLogic.players) {
			temp_string = roles[count].Split('-');
			obj.membership_card = temp_string[0];
			obj.profile_card = temp_string[1];

			count++;
		}
	}

	public void reveal_player_role () {

		player_role_display.text = GameLogic.players[player_no_2_displayer].profile_card + " " + "(" +  GameLogic.players[player_no_2_displayer].membership_card + ")";
	}

	public void next_player_2_reveal () {
		player_no_2_displayer ++;

		if (player_no_2_displayer < GameLogic.players.Count) {
			player_role_display.text = " ";
			player_name_display.text = GameLogic.players[player_no_2_displayer].user_name;
		} else {
			SceneManager.LoadScene ("Board");
		}
	}

}
