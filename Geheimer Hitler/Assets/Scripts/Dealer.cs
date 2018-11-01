using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour {

	public struct card  {
		public string legislation_type;
		public bool card_drawn;


	}

	public static int deck_idx;

	static public card [] deck;
	


	// Use this for initialization
	void Start () {

		deck = new card [17];
		
		for (byte i = 0; i < deck.Length; i++) {
			deck[i].card_drawn = false;

			if (i <= 10) {
				deck[i].legislation_type = "Facist";
			} else {
				deck[i].legislation_type = "Liberal";
			}

		}

		shuffle_deck ();


		

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void shuffle_deck () {

		Debug.Log("Shuffle called");

		Random rnd = new Random ();

		for (int i = deck.Length; i > 0; i--) {
			int j = Random.Range(0, i);
			card k = deck [j];

			deck[j] = deck [i-1];
			deck[i-1] = k;

		}
	}
}
