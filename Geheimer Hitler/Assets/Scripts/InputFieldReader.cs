using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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


	short screen_w, left_padding, top_padding;

	[SerializeField]
	Transform my_canvas;

	public Sprite button_ready;
	public Sprite button_not_ready;

	public GameObject start_txt;
	

	

	void Awake () {


		//start_game_but.GetComponent<Image> ().color = Color.red;

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
	
	public void broadcast_text_Input (int ID) {
		//Debug.Log(TextMeshProTexts[ID].transform.parent.gameObject.name + "writes: " + TextMeshProTexts[ID].text);
		
		local_gamelogic.player_registration (ID, TextMeshProTexts[ID].text);
		TextMeshProTexts[ID].text = GameLogic.players[ID].user_name;
		Image  temp_but = player_UIs[ID].GetComponentsInChildren<Image>()[1];

		if (TextMeshProTexts[ID].text != "" && TextMeshProTexts[ID].text != " ") {
			Debug.Log(player_UIs[ID].GetComponentsInChildren<Image>()[1].name);

			GameLogic.players[ID].locked_in = true;

			temp_but.GetComponent<Image>().sprite = locked_in;

			GameLogic.locked_in_players ++;
		

		} else
		{
			
			 temp_but.GetComponent<Image>().sprite = not_locked_in;
			 GameLogic.players[ID].locked_in = false;
			 GameLogic.locked_in_players --;

			 
		}

		foreach (Player obj in GameLogic.players)
		{
			Debug.Log("User: " + obj.user_name + "´s looked in status is currently: " + obj.locked_in);
		}

		Debug.Log("---------------------------");

		local_gamelogic.current_lock_status ();

		

		if (GameLogic.all_players_are_ready && TextMeshProTexts[ID].text != "") {
			start_txt.GetComponent<TMP_Text>().color = new Color(1,1,1,1);
			start_txt.transform.localPosition = new Vector3 (0,6.7f,0);
			start_game_but.GetComponent<Image> ().sprite = button_ready;
			start_game_but.GetComponent<Image> ().color = new Color(1,1,1,1);
		} else {
			start_txt.GetComponent<TMP_Text>().color = new Color(1,1,1,0.5f);
			start_txt.transform.localPosition = new Vector3 (0,2.4f,0);
			start_game_but.GetComponent<Image> ().sprite = button_not_ready;
			start_game_but.GetComponent<Image> ().color = new Color(1,1,1,0.5f);
		}

		
		
		
	}

	public void broadcast_lock_status (int ID) {
		Button temp_but = player_UIs[ID].GetComponentInChildren<Button>();

		if (GameLogic.players[ID].user_name != null && GameLogic.players[ID].user_name != "") {
			//local_gamelogic.current_lock_status (ID);

			if (GameLogic.players[ID].locked_in) {
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
		if (GameLogic.all_players_are_ready) {
			start_txt.transform.localPosition = new Vector3 (0,2.4f,0);
			start_game_but.GetComponent<Image>().sprite = button_not_ready;
			//SceneManager.LoadScene ("Role_delegation");
			StartCoroutine("scene_transition");
			
		}	
	}		

	public void back_2_start_screen () {
		SceneManager.LoadScene("Start_screen");
		
	}


	IEnumerator scene_transition () {
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("Role_delegation");
	}
}

