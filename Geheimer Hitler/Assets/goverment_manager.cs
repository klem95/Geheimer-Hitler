using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goverment_manager : MonoBehaviour {

	public delegate void goMa_calls_for_update();
	public static event goMa_calls_for_update election_outcome;
	public static event goMa_calls_for_update failed_election;
	public static event goMa_calls_for_update passed_election;

	[Header ("Players")]
	public Button [] player_buttons;

	[Header ("Color Settings")]

	public Color pres_color;
	public Color prev_chanc_color;
	public Color norminated_chanc_color;

	public Color default_color;

	int current_selected_player_ID;

	[Header ("Interface")]
	public Button reject_nominee;
	public Button accept_nominee;

	[Header ("Headlines")]
	public TMPro.TMP_Text Chancellor_txt;
	public TMPro.TMP_Text President_txt;


	Color transparent_color = new Color (1,1,1,0.5f);
	Color filled_color  = new Color (1,1,1,1);

	void Awake() {
		
	
	}

	// Use this for initialization
	void Start () {

		game_manager.goverment_update += present_goverment;
		initialize_goverment (); 
		Dealer.legislation_played += present_goverment;
		
	}
	
	// Update is called once per frame
	void Update () {

		}

	void present_goverment () {

		int counter = 0;

		foreach (Button button_p in player_buttons)
		{
			if (counter < GameLogic.players.Count) {
				if(counter == game_manager.previous_chanc_id &&  counter != game_manager.current_pres_id) {
					player_buttons[game_manager.previous_chanc_id].GetComponent<Image>().color = prev_chanc_color;
				} else if (counter == game_manager.current_pres_id) {
					player_buttons[game_manager.current_pres_id].GetComponent<Image>().color = pres_color;
				} else
				{
					button_p.GetComponent<Image>().color = default_color;
				}
			} 
			counter ++;
		}
	}


	void initialize_goverment () {

		int counter = 0;

		foreach (Button button_p in player_buttons)
		{
			if (counter < GameLogic.players.Count) {
				button_p.interactable = true;
				button_p.GetComponentInChildren<TMPro.TMP_Text>().text = GameLogic.players[counter].user_name;
			} else {
				button_p.interactable = false;
				button_p.GetComponentInChildren<TMPro.TMP_Text>().text = " ";
			}

			counter ++;
		}

		player_buttons[game_manager.current_pres_id].GetComponent<Image>().color = pres_color;
		Chancellor_txt.text = "Undecided";

		President_txt.text = game_manager.current_pres_username;

		vote_interactable(false);
		

	}

	void vote_interactable (bool status) {

		accept_nominee.interactable = status;
		reject_nominee.interactable = status;

		if (status) {
			accept_nominee.GetComponentInChildren<TMPro.TMP_Text>().color = filled_color;
			reject_nominee.GetComponentInChildren<TMPro.TMP_Text>().color = filled_color;
		} else {
			accept_nominee.GetComponentInChildren<TMPro.TMP_Text>().color = transparent_color;
			reject_nominee.GetComponentInChildren<TMPro.TMP_Text>().color = transparent_color;
		}
		
		
	}

	
	public void player_icon_selcted (int ID) 
	{

		if (game_manager.current_pres_id != ID && game_manager.previous_chanc_id != ID) {
			int count = 0;
			current_selected_player_ID = ID;

			foreach (Button obj in player_buttons)
			{
				if (obj.interactable) {
					if (count == current_selected_player_ID && current_selected_player_ID != game_manager.current_pres_id && count != game_manager.previous_chanc_id && count != game_manager.previous_chanc_id) {
						obj.GetComponent<Image>().color = norminated_chanc_color;
					} else if (count == game_manager.current_pres_id) {
						player_buttons[game_manager.current_pres_id].GetComponent<Image>().color = pres_color;
					} else if (count == game_manager.previous_chanc_id && game_manager.previous_chanc_id > -1 && count != game_manager.current_pres_id) {
						player_buttons[game_manager.previous_chanc_id].GetComponent<Image>().color = prev_chanc_color;
					} else {
						obj.GetComponent<Image>().color = default_color;
					}
				}
				count++;
				vote_interactable(true);
			}
		} 
	}


	public void chancellor_elected () 
	{
		GameLogic.players[current_selected_player_ID].chancellor = true;
		game_manager.current_chanc_username = GameLogic.players[current_selected_player_ID].user_name;
		game_manager.current_chanc_id = GameLogic.players[current_selected_player_ID].ID;

		Chancellor_txt.text = GameLogic.players[current_selected_player_ID].user_name;

		game_manager.norminate_chan = false;
		game_manager.draw_cards = true;

		game_manager.prev_button =  "Nominate";

		election_outcome();
		passed_election();
		
	}

	public void chancellor_rejected () {
		Chancellor_txt.text = "Undecided";

		election_outcome();
		failed_election();
	}



	


}
