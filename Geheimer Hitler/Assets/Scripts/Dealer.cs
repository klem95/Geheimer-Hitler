using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Dealer : MonoBehaviour {

	public delegate void dealer_call_for_update ();
	public static event dealer_call_for_update legislation_played;

	public delegate void activate_leg_card(string played_card);
	public static event activate_leg_card change_board;
	
	public struct card  {
		public string legislation_type;
		public bool card_drawn;


	}

	public static int deck_idx;
	int card_to_discard_idx;

	static public card [] deck;

	public Button action_button;

	bool discard_legi;


	public Button [] card_placeholders;

	static int draw_count;

	public Sprite facist_Sprite;
	public Sprite liberal_Sprite;

	public Sprite defualt_Sprite;	

	string chosen_card;

	public Image [] facist_board_img;
	public Image [] liberal_board_img;

	public GameObject dropdown_side;

	public GameObject game_man;
	
	


	// Use this for initialization
	void Start () {

		discard_legi = false;

		deck = new card [17];
		
		for (byte i = 0; i < deck.Length; i++) {
			deck[i].card_drawn = false;

			if (i <= 10) {
				deck[i].legislation_type = "Facist";
			} else {
				deck[i].legislation_type = "Liberal";
			}

		}

		action_button.onClick.AddListener(draw_top_3_cards);
		shuffle_deck ();

		
	for (int i = 0; i < 3; i++)
	{
		add_listiner(i);
	}
				
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void shuffle_deck () {
		Debug.Log("shuffle deck");

		draw_count = 0;

//		Debug.Log("Shuffle called");

		Random rnd = new Random ();

		for (int i = deck.Length; i > 0; i--) {
			int j = Random.Range(0, i);
			card k = deck [j];

			deck[j] = deck [i-1];
			deck[i-1] = k;

		}

	}

	void draw_top_3_cards () {
			discard_legi = true;
		

			if (draw_count+3 >= deck.Length) {
				shuffle_deck();

				
			}

			foreach (Button obj in card_placeholders)
			{
				draw_count ++;

				obj.GetComponent<Image>().color = new Color(1,1,1,1);
				if (deck[draw_count].legislation_type == "Facist") {
					obj.GetComponent<Image>().sprite = facist_Sprite;
					
				} else {
					obj.GetComponent<Image>().sprite = liberal_Sprite;
				}

				obj.interactable = true;
			}


		action_button.onClick.RemoveAllListeners();
		action_button.GetComponentInChildren<TMPro.TMP_Text>().text = "Discard";
		action_button.onClick.AddListener(discard_card);
		//action_button.GetComponentInChildren<TMPro.TMP_Text>().text = "Activate";
		//action_button.onClick.AddListener(activate_card);
		action_button.interactable = false;		
		
	}

	void add_listiner (int input) {
		card_placeholders[input].onClick.AddListener(() => select_card(input));
		card_placeholders[input].interactable = false;
	}

	void select_card (int buttonNo) {

		if (discard_legi) {
			card_to_discard_idx = buttonNo;
		}
		

		action_button.interactable = true;		

		for (int i = 0; i < card_placeholders.Length; i++)
		{
			if (i != buttonNo && i != card_to_discard_idx){
				card_placeholders[i].GetComponent<Image>().color = new Color (1,1,1,0.5f);
			} else if (!discard_legi && i == card_to_discard_idx) {
				card_placeholders[i].GetComponent<Image>().color = new Color (0,0,0,0);

				Debug.Log("This card is discarded, no. :" + buttonNo);
			} else {
				card_placeholders[i].GetComponent<Image>().color = new Color (1,1,1,1);
				chosen_card = card_placeholders[i].GetComponent<Image>().sprite.name;
			}
		}

		

	}
	
	void activate_card () {

		if (chosen_card == "Facist") {
			
			//facist_board_img[GameLogic.facist_card_played].sprite = facist_Sprite;
			//facist_board_img[GameLogic.facist_card_played].color = new Color (1,1,1,1);
			//game_manager.current_facist_pos ++;

			//game_board.played_facist_card = true;
			//game_board.played_liberal_card = false;

			change_board ("Facist");

			GameLogic.facist_card_played ++;

		} else
		{
			//liberal_board_img[GameLogic.liberal_card_played].sprite = liberal_Sprite;
			//liberal_board_img[GameLogic.liberal_card_played].color = new Color (1,1,1,1);
			//game_manager.current_facist_pos ++;

			//game_board.played_facist_card = false;
			//game_board.played_liberal_card = true;

			change_board ("Liberal");

			GameLogic.liberal_card_played ++;
		}

		foreach (Button obj in card_placeholders)
		{
			obj.GetComponent<Image>().color = new Color (0,0,0,0.5f);
		}

		//game_man.GetComponent<game_manager>().draw_cards = false;
		//game_man.GetComponent<game_manager>().norminate_chan = true;

		//game_man.GetComponent<game_manager>().prev_button = "Draw"; 

		//dropdown_side.GetComponent<dropdown_side>().activate_dropdown_down();
		//game_man.GetComponent<game_manager>().decision_tree();

		game_manager.draw_cards = false;
		game_manager.norminate_chan = true;
		
		game_manager.prev_button = "Draw";

		legislation_played ();

		action_button.onClick.RemoveAllListeners();
		action_button.GetComponentInChildren<TMPro.TMP_Text>().text = "Reveal";
		action_button.onClick.AddListener(draw_top_3_cards);

	}

	void discard_card () {

		card_placeholders[card_to_discard_idx].interactable = false;
		card_placeholders[card_to_discard_idx].GetComponent<Image>().color = new Color (0,0,0,0);
		discard_legi = false;

		action_button.onClick.RemoveAllListeners();
		action_button.GetComponentInChildren<TMPro.TMP_Text>().text = "Reveal";
		action_button.onClick.AddListener(reveal_options);

		hide_cards ();
	}

	void hide_cards () 
	{
		foreach (Button obj in card_placeholders)
		{
			obj.GetComponent<Image>().color = new Color (0,0,0,0);
		}

	}

	void reveal_options () {

		action_button.onClick.RemoveAllListeners();
		action_button.GetComponentInChildren<TMPro.TMP_Text>().text = "Activate";
		action_button.onClick.AddListener(activate_card);

		int count = 0;
		foreach (Button obj in card_placeholders)
			{
				if (count == card_to_discard_idx) {
					obj.GetComponent<Image>().color = new Color (0,0,0,0);
					Debug.Log("hide me");
				} else
				{
					obj.GetComponent<Image>().color = new Color (1,1,1,1);
				}
				count++;
			}

		

	}

}
