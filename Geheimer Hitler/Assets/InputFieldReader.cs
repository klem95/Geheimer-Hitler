﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputFieldReader : MonoBehaviour {

	[SerializeField]
	List <TMP_InputField> TextMeshProTexts; 

	List <GameObject> player_UIs;

	[SerializeField]
	Sprite locked_in;

	[SerializeField]
	Sprite not_locked_in;

	[SerializeField]
	Button start_game_but;

	Color Buttoncolor;

	GameLogic local_gamelogic;


	int screen_w, left_padding, top_padding;

	[SerializeField]
	Transform my_canvas;

	

	

	void Awake () {


		start_game_but.GetComponent<Image> ().color = Color.red;

		player_UIs = new List<GameObject> ();

		foreach (Transform child in my_canvas) {

			//Debug.Log ("here is a child: " + child.name);

			char temp_char = child.name[0];
			if (char.GetNumericValue(temp_char) >= GameLogic.players.Count) { 
					//child.transform.parent.gameObject.SetActive(false);
					child.gameObject.SetActive(false);
			} else {
				TextMeshProTexts.Add(child.GetComponentInChildren<TMP_InputField>());
				player_UIs.Add (child.gameObject);
			}	
		}
	}

	// Use this for initialization
	void Start () {

	

		
		local_gamelogic = GameObject.Find("Game_master").GetComponent<GameLogic> ();

		
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

	public void broadcast_text_Input (int ID) {
		//Debug.Log(TextMeshProTexts[ID].transform.parent.gameObject.name + "writes: " + TextMeshProTexts[ID].text);
		if (!GameLogic.players[ID].locked_in) {
			local_gamelogic.player_registration (ID, TextMeshProTexts[ID].text);
		} {
			TextMeshProTexts[ID].text = GameLogic.players[ID].user_name;
		}
		
	}

	public void broadcast_lock_status (int ID) {
		Button temp_but = player_UIs[ID].GetComponentInChildren<Button>();

		if (GameLogic.players[ID].user_name != null && GameLogic.players[ID].user_name != "") {
			local_gamelogic.current_lock_status (ID);

			if (GameLogic.players[ID].locked_in) {
				Debug.Log ("Chaning to true");
				temp_but.GetComponent<Image>().sprite = locked_in;
			} else {
				temp_but.GetComponent<Image>().sprite = not_locked_in;
			}
		}

		if (GameLogic.all_players_are_ready) {
			start_game_but.GetComponent<Image> ().color = Color.green;
		} else {
			start_game_but.GetComponent<Image> ().color = Color.red;
		}
	}


	public void begin_game () {

	}

		
}

