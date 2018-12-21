using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class game_board : MonoBehaviour {


	[Header("Legislations boards")]
	public Image [] facist_cards_in_playholders;

	public Image [] liberal_cards_in_playholders;

	public Image [] election_tracker;


	[Header("Sprites")]
	public Sprite facist_card_sprite;

	public Sprite liberal_card_sprite;


	//static public bool played_facist_card;
	//static public bool played_liberal_card;
	

	// Use this for initialization
	void Start () {

		goverment_manager.failed_election += electiontracker_update;

		Dealer.change_board += board_update_legislations;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void board_update_legislations (string input) {

		if (input == "Facist") {
			facist_cards_in_playholders[GameLogic.facist_card_played].sprite = facist_card_sprite;
			facist_cards_in_playholders[GameLogic.facist_card_played].color = new Color (1,1,1,1);
		} else
		{
			liberal_cards_in_playholders[GameLogic.liberal_card_played].sprite = liberal_card_sprite;
			liberal_cards_in_playholders[GameLogic.liberal_card_played].color = new Color (1,1,1,1);
		}
	}


	void electiontracker_update () {
		election_tracker[GameLogic.election_tracker_counter].color = new Color (0.8584906f,0.1741278f,0.6150833f,1);
		GameLogic.election_tracker_counter++;
	}

}
