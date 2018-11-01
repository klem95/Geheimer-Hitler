using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/////////// Status (29/10) ///////////
/*


 */

public class GUI_man : MonoBehaviour {

	[SerializeField]
	TMP_Text [] GUI_text;
	[SerializeField]
	GameObject dropdown,ui_inter;

	[SerializeField]
	float animation_t;

	bool being_g;

	bool locked_in;
	bool animate_elec;
	Narative_manager narrator;

	string sitting_pres, sitting_chanc;

	[SerializeField]
	GameObject [] player_buttons;

	byte sitting_pres_id, sitting_chanc_id;
	GameObject [] vote_b;
	GameObject [] player_b;

	GameObject  begin_b;

	GameObject [] cards_img;

	[SerializeField]
	GameObject [] facist_legi_slots;
	[SerializeField]
	GameObject [] liberal_legi_slots;
	[SerializeField]
	GameObject [] legislation_tracker;


	bool display_candi;

	int chosen_card_id;
	string chosen_card_type;


	void Awake () {

		being_g = true;
		locked_in = false;
		byte rnd_no = (byte)Random.Range(0,GameLogic.players.Count);

		sitting_chanc= "";

		GUI_text[0].text = "Let the game begin";
		GUI_text[1].text = "The first player to rule as presendent is:";
		GUI_text[2].text = GameLogic.players[rnd_no].user_name;

		GameLogic.players[rnd_no].president = true;

		sitting_pres = GameLogic.players[rnd_no].user_name;
		sitting_pres_id =  GameLogic.players[rnd_no].ID;

		vote_b = GameObject.FindGameObjectsWithTag("vote_b");
		player_b = GameObject.FindGameObjectsWithTag("player_b");

		cards_img = GameObject.FindGameObjectsWithTag("drawn_cards");
		

		begin_b = GameObject.FindGameObjectWithTag("begin_b");

		display_candi = true;

		activate_gameobject (vote_b, false);
		activate_gameobject (cards_img, false);
		activate_gameobject (player_b, false);

		narrator = new Narative_manager ();

	}

	public void animate_Ui (string _dir) {

		if (_dir == "up") {
			StartCoroutine (animate_drop_down("up"));
		} else {
			StartCoroutine (animate_drop_down("down"));
		}
	}

	////////////////////////////// UI Animations \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

			IEnumerator animate_drop_down (string _dir) {

			Transform dropdown_pos = dropdown.GetComponent<Transform>();

				if (_dir == "up") {
					while (dropdown_pos.position.x < 960) {
						dropdown_pos.position = new Vector3 (dropdown_pos.position.x+20,dropdown_pos.position.y,dropdown_pos.position.z);
						yield return new WaitForSeconds(animation_t);
					}
					StartCoroutine (animate_UI_interater("down"));
				} else {
					StartCoroutine (animate_UI_interater("up"));
					while (dropdown_pos.position.x > 320) {
						dropdown_pos.position = new Vector3 (dropdown_pos.position.x-20,dropdown_pos.position.y,dropdown_pos.position.z);
						yield return new WaitForSeconds(animation_t);
					}
					
				}
			}

			IEnumerator animate_UI_interater (string _dir) {
				Transform UI_inter = ui_inter.GetComponent<Transform> ();

				if (_dir == "down") { 
					while (UI_inter.position.x > 650) {
						UI_inter.position = new Vector3 (UI_inter.position.x -20,UI_inter.position.y,UI_inter.position.z);
						yield return new WaitForSeconds(animation_t);
					}
				} else {

					while (UI_inter.position.x < 770) {
						UI_inter.position = new Vector3 (UI_inter.position.x +20,UI_inter.position.y,UI_inter.position.z);
						yield return new WaitForSeconds(animation_t);
					}
					
				}
			}

			IEnumerator display_passed_legi (GameObject img_slot, Color input_col) {	
				while (input_col.a <= 1f) {
					input_col.a += 0.01f;
					img_slot.GetComponent<Image> ().color = input_col;

					yield return new WaitForSeconds(animation_t);
				}
			}

			IEnumerator animate_election_track () {

				foreach (GameObject obj in legislation_tracker) {
					Color tmp_col = obj.GetComponent<Image>().color;
					while (obj.GetComponent<Image>().color.r < 0.6226) {
						tmp_col.r += 0.1f;
						tmp_col.g += 0.1f;
						tmp_col.b += 0.1f;
						obj.GetComponent<Image>().color = tmp_col;
						yield return new WaitForSeconds(animation_t);
					}
					

				}	
			}




			

			public void display_legi () {
				StartCoroutine (animate_drop_down ("up"));
				StartCoroutine (animate_UI_interater ("down"));

				// if (chosen_card_type) {
				// 	Color temp = cards_img[chosen_card_id].GetComponent<Image>().color;
				// }
				// Color temp = cards_img[chosen_card_id].GetComponent<Image>().color;
				// temp.a = 0;

				if (chosen_card_type == "Facist") {
					Color temp = new Color (255,0,0,0);
					StartCoroutine (display_passed_legi(facist_legi_slots[GameLogic.played_facist_cards],temp));
					GameLogic.played_facist_cards ++;
				} else {
					Color temp = new Color (0,0,255,0);
					StartCoroutine (display_passed_legi(liberal_legi_slots[GameLogic.played_liberal_cards],temp));
					GameLogic.played_liberal_cards ++;
				}
				

			}


	////////////////////////////// UI Content/interaction \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


		public void display_candidates () {

			if (display_candi) { 
				string [] temp_string = narrator.intro_message();
				GUI_text[0].text = temp_string[0];
				GUI_text[1].text = temp_string[1];

				byte count = 0;
				foreach(GameObject obj in player_buttons) {
					if (being_g) {
						obj.SetActive(true);
					}
					obj.GetComponentInChildren<Text> ().text = GameLogic.players[count].user_name;

					if (GameLogic.players[count].president) {
					//	Debug.Log (GameLogic.players[count].user_name);
						obj.GetComponent<Image>().color = Color.red;
					} else {
						obj.GetComponent<Image>().color = Color.white;
					}
					count++;
				}
				being_g =false;
				display_candi = false;

				begin_b.SetActive(false);
				activate_gameobject(vote_b,true);
			}
		}


		public void chose_chan (int idx) {
			if (idx != sitting_pres_id) {
				sitting_chanc = GameLogic.players[idx].user_name;
				foreach (GameObject obj in player_b) {
					string tmp_name = obj.GetComponentInChildren<Text>().text;

					if (tmp_name == sitting_chanc) {
						obj.GetComponent<Image>().color = Color.green;
					} else if (tmp_name == sitting_pres){
						obj.GetComponent<Image>().color = Color.red;
					} else {
						obj.GetComponent<Image>().color = Color.white;
					}
			}
			
			GameLogic.players[idx].chancellor = true;
			sitting_chanc_id = GameLogic.players[idx].ID;
			//Debug.Log ("Player: " + GameLogic.players[idx].user_name + "has been chosen as chanclor");	
	
			}
		}

		public void vote_result (string _result) {

			if (_result == "ja") {
					if (sitting_chanc != "") {
					GameLogic.logged_data.Add (new Log_data(sitting_pres,sitting_chanc));
					//StartCoroutine (animate_drop_down ("up"));
					//StartCoroutine (animate_UI_interater ("down"));

					activate_gameobject (vote_b, false);
					activate_gameobject (player_b, false);

					GUI_text[0].text = "";
					GUI_text[1].text = "";
					GUI_text[2].text = "";

					passing_legislations ();
				}
			} else {
					legislation_tracker[GameLogic.elections_failed].GetComponent<Image>().color = Color.black;
				if (GameLogic.elections_failed < 3) {
					assign_nxt_pres (sitting_pres_id);
				} else {
					play_top_legi ();
				}

				GameLogic.election_tracker ();
			
				// sitting_chanc = "";
				// sitting_chanc_id = 12;
				
			}

		}

	public void assign_nxt_pres (int _former_pres_id) {
		GameLogic.players[_former_pres_id].president = false;

		if (_former_pres_id == GameLogic.players.Count-1) {
			_former_pres_id = -1;
		}
		
		int nxt_pres_id = _former_pres_id + 1;
		GameLogic.players[nxt_pres_id].president = true;

		sitting_pres = GameLogic.players[nxt_pres_id].user_name;
		sitting_pres_id = GameLogic.players[nxt_pres_id].ID;

		sitting_chanc = "";
		sitting_chanc_id = 12;


		//Debug.Log ("new pres " + sitting_pres );

		display_candi = true;
		display_candidates ();

	}

	
	public void display_drawn_cards_1 () {

		activate_gameobject (cards_img,true);

		begin_b.GetComponentInChildren<Text>().text = "Discard";
		begin_b.GetComponent<Button>().onClick.RemoveAllListeners();
		begin_b.GetComponent<Button>().onClick.AddListener (discard);
	}

	public void display_drawn_cards_2 () {

		for (int i = 0; i <= 2; i++) {
			if (i != chosen_card_id) {
				cards_img[i].SetActive(true);
			}
		}

		begin_b.GetComponentInChildren<Text>().text = "Pass";
		begin_b.GetComponent<Button>().onClick.RemoveAllListeners();
		begin_b.GetComponent<Button>().onClick.AddListener(display_legi);
	}

	public void chosen_card (int _card_id) {
			chosen_card_type =  cards_img [_card_id].name;
			chosen_card_id = _card_id;
		}

	////////////////////////////// Game mechanics \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

		public void passing_legislations () {

			string [] temp_string = narrator.show_drawn_cards ();
			GUI_text[0].text = temp_string [0];
			GUI_text[1].text = temp_string [1];

			begin_b.SetActive(true);
			begin_b.GetComponentInChildren<Text>().text = "Reveal";

			begin_b.GetComponent<Button>().onClick.RemoveAllListeners();
			begin_b.GetComponent<Button>().onClick.AddListener (display_drawn_cards_1);

			if (Dealer.deck_idx + 3 > 17) {
				Dealer.deck_idx = 0;
				Dealer.shuffle_deck ();
			}

			

			for (int i = 0; i <= 2; i++) {
				Dealer.deck_idx ++;
				Dealer.deck[Dealer.deck_idx].card_drawn = true;

				if (Dealer.deck[Dealer.deck_idx].legislation_type == "Facist") {
					cards_img[i].name = "Facist";
					cards_img[i].GetComponent<Image>().color = Color.red;
				} else {
					cards_img[i].name = "Liberal";
					cards_img[i].GetComponent<Image>().color = Color.blue;
				}

			}

			Debug.Log("Card idx: " + Dealer.deck_idx  );
		}

		public void play_top_legi () {
			chosen_card_type = Dealer.deck[Dealer.deck_idx].legislation_type;
			animate_elec = true;
			display_legi ();
			Dealer.deck_idx ++;
			
		}

		public void discard () {
		
			activate_gameobject(cards_img,false);

			string [] temp_string = narrator.show_drawn_cards ();
			GUI_text[0].text = temp_string[2];
			GUI_text[1].text = temp_string[3];

			begin_b.GetComponentInChildren<Text>().text = "Reveal";
			begin_b.GetComponent<Button>().onClick.AddListener (display_drawn_cards_2);
		}

		public void initiate_next_round () {
			being_g = true;
			display_candi = true;
			activate_gameobject(cards_img,false);
			assign_nxt_pres (sitting_pres_id);
			display_candidates ();

			if (animate_elec) {
				animate_elec = false;
				StartCoroutine(animate_election_track());

			}
		}

	////////////////////////////// Others \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

		public void activate_gameobject (GameObject [] _gameobj, bool _status) {
			foreach (GameObject obj in _gameobj) {
				obj.SetActive(_status);
			}

		}
}
