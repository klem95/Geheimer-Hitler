using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class game_manager : MonoBehaviour {


	public delegate void gm_calls_for_update ();
	static public event gm_calls_for_update election_failed_update_board;
	static public event gm_calls_for_update norminating_player;

	static public event gm_calls_for_update goverment_update;


	[Header("Text")]
	public TMPro.TMP_Text president_txt;
	public TMPro.TMP_Text Chancellor_txt;

	[Header("Icons")]
	public Sprite investigate;
	public Sprite elect;

	public Sprite top_card;
	public Sprite execute;

	public Image [] powers;


	[Header("Interface")]
	//public GameObject [] interface_buttons;

	public Sprite pressed; 
	public Sprite not_pressed; 

	Button [] interface_buttons;

	public Button [] player_buttons;

	GameObject [] goverment_txt;

	public Button [] vote_buttons;

	public Button action_button;

	// Goverment // 

	static public int current_chanc_id;
	static public  int previous_chanc_id;
	static public int current_pres_id;
	static public  int previous_pres_id;
	static public int  selected_player_id;
	
	static public string current_chanc_username;
	static public string current_pres_username;
	static public string previous_pres_username;

	static public string previous_chanc_username;

	static public int current_facist_pos;

	static public bool norminate_chan;
	public bool power_active;
	static public bool draw_cards;

	bool game_started;


	static public string prev_button;

	drop_down_menu_top act_dropdown_up;

	//Color pres_color = new Color (0.6698113f,0.135858f,0.1541417f,0.5f);



	void Awake () {


		int rnd = Random.Range(0,GameLogic.players.Count-1);
		
		GameLogic.players [rnd].president = true;
		current_pres_id = rnd;

		

		game_started = true;
		previous_chanc_id = -1;
		previous_pres_id = -1;

		current_facist_pos = 0;
		
		//vote_buttons[0].interactable = false;
		//vote_buttons[1].interactable = false;

		// foreach (Button obj in vote_buttons)
		// {
		// 	obj.interactable = false;
		// 	obj.GetComponentInChildren<TMPro.TMP_Text>().color -= new Color (0,0,0, 0.5f); 
		// }
	

		if (GameLogic.game_size ==  "small") {
			Sprite [] tmp_power = {top_card,execute,execute};
			setup_board(tmp_power,GameLogic.game_size);

		} else if (GameLogic.game_size ==  "medium") {
			Sprite [] tmp_power = {investigate,elect,execute,execute};
			setup_board(tmp_power,GameLogic.game_size);
		} else
		{
			Sprite [] tmp_power = {investigate,investigate,elect,execute,execute};
			setup_board(tmp_power,GameLogic.game_size);
		}

		interface_buttons = GameObject.FindGameObjectWithTag("interaction_bar").GetComponentsInChildren<Button>();
		goverment_txt =  GameObject.FindGameObjectsWithTag("goverment_txt");

		current_pres_username = GameLogic.players [rnd].user_name;
		
	//	display_goverment(GameLogic.players [rnd].user_name, "Undecided");

		interface_buttons[0].GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition -= new Vector3 (0,5,0);
		

		foreach(Button obj in interface_buttons) {
			if(obj.name != "Nominate") {
				obj.GetComponent<Button>().interactable = false;
			}
		}

	}

	// Use this for initialization
	void Start () {

		goverment_manager.failed_election += select_president;
		goverment_manager.passed_election += decision_tree;
		Dealer.legislation_played += decision_tree;
		
		norminate_chan = true;

		decision_tree ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setup_board (Sprite [] power_sprites, string game_size) {

		int count;
		if (game_size == "small") {
			count = 2;
		} else if (game_size == "medium") {
			powers[1].color += new Color (0,0,0,1);
			count = 1;
		} else
		{
			powers[0].color += new Color (0,0,0,1);
			powers[1].color += new Color (0,0,0,1);
			count = 0;
		}
		
		foreach (Sprite obj in power_sprites)
		{
			powers[count].sprite = obj;
			count++;
		}
	}


	// ---------------------- Interface functions ----------------------  //

	void disable_buttons (string active_button) {

		//player_buttons[current_pres_id].GetComponent<Image>().color = pres_color;

		if (!game_started) 
		{ 
			foreach (Button obj in interface_buttons)
			{
				if(obj.name == prev_button) {
					obj.GetComponent<Image>().sprite = pressed;
				//	obj.GetComponent<Image>().color -= new Color (0,0,0,0.5f);
					obj.GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition -= new Vector3 (0,5,0); 
					obj.GetComponentInChildren<TMPro.TMP_Text>().color = new Color (1,1,1,0.5f);
					obj.interactable = false;
				} else if (obj.name == active_button) {				
					obj.GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition += new Vector3 (0,5,0);
					obj.GetComponent<Image>().sprite = not_pressed;
					obj.GetComponent<Button>().image.color = new Color (1,1,1,1f);
					obj.GetComponentInChildren<TMPro.TMP_Text>().color = new Color (1,1,1,1f);
					obj.interactable = true;
				} 
			}
		} else {
			interface_buttons[0].GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition += new Vector3 (0,5,0);
			interface_buttons[0].GetComponent<Image>().sprite = not_pressed;
			interface_buttons[0].GetComponentInChildren<TMPro.TMP_Text>().color = new Color (1,1,1,1f);
			interface_buttons[0].interactable = true;

			

			game_started = false;
		}
	}




	// void display_goverment (string current_pres, string current_chanc) {

	// 	goverment_txt[1].GetComponent<TMPro.TMP_Text>().text = current_pres;
	// 	goverment_txt[0].GetComponent<TMPro.TMP_Text>().text = current_chanc;

	// }

/* 
	public void player_icon_selcted (int ID) {
		int count = 0;
		selected_player_id = ID;
		
		drop_down_menu_top.remote_activation = true;


		foreach (Button obj in player_buttons)
		{
			if (count == selected_player_id && selected_player_id != current_pres_id) {
				obj.GetComponent<Image>().color = Color.green;
			} else if (count == current_pres_id) {
				//player_buttons[current_pres_id].GetComponent<Image>().color = pres_color;
			} else {
				obj.GetComponent<Image>().color = new Color (0.2924528f, 0.2690014f,0.2690014f);
			}

			count++;
		}


		if (norminate_chan) {
			foreach (Button obj in vote_buttons)
			{
				obj.interactable = true;
				obj.GetComponentInChildren<TMPro.TMP_Text>().color += new Color(0,0,0,0.5f);
			}

	

			// vote_buttons[0].onClick.AddListener (chancellor_selected);
		}
	}

	*/

	// ------------------------------------------------------------------  //

	// ---------------------- Game logic funtions ----------------------  //

	void select_president () {

		previous_pres_id = current_pres_id;
		previous_pres_username = current_pres_username;

		previous_chanc_id = current_chanc_id;
		previous_chanc_username = current_chanc_username;

		current_chanc_id = -1;
		current_chanc_username = "Undecided";

		goverment_txt[0].GetComponent<TMPro.TMP_Text>().text = current_chanc_username;

		if (previous_pres_id + 1 != GameLogic.players.Count) {
			current_pres_id += 1;
		} else
		{
			current_pres_id = 0;
		}

		GameLogic.players [current_pres_id].president = true;

		president_txt.text = GameLogic.players [current_pres_id].user_name;

		goverment_update ();

	}

	// void chancellor_selected () {
	// 	GameLogic.players[selected_player_id].chancellor = true;
	// 	Chancellor_txt.text = GameLogic.players[selected_player_id].user_name;

	// 	norminate_chan = false;
	// 	draw_cards = true;

	// 	prev_button = "Nominate";

	// 	decision_tree();
		
	// }

	// void vote_failed () {
	// 	Debug.Log("2 bad son!");

	// 	election_failed_update_board();

	// 	select_president ();
	// 	norminate_chan = true;
	// }

	public void decision_tree () {
	

		if (GameLogic.board_layout.check_4_power(current_facist_pos) != "none") {
			Debug.Log("Power up detected");
			power_active = true;
		} else {
			Debug.Log("No power up detected");
			power_active = false;
		}

		if (norminate_chan) {

			if(!game_started) {
				select_president ();
			}

//			norminating_player();


			disable_buttons ("Nominate");
			action_button.gameObject.SetActive(false);

			foreach (Button obj in vote_buttons)
			{
				
				//obj.gameObject.SetActive(true);
				//obj.interactable = false;

				//obj.GetComponentInChildren<TMPro.TMP_Text>().color -= new Color (0,0,0, 0.5f); 

				
			}

			//vote_buttons[0].onClick.AddListener (chancellor_selected);
			//vote_buttons[1].onClick.AddListener (vote_failed);
	

		} else if (draw_cards) {

			disable_buttons("Draw");

		} else if (power_active) {

		}


	

	}

	// ------------------------------------------------------------------  //


	

}
