using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class Role_delegation : MonoBehaviour {

	string [,] potical_profile = {
		{"Facist","Adolf Hitler"},
		{"Facist","Joseph Goebbels"},
		{"Facist","Heinrich Himmler"},
		{"Facist","Hermann W. Göring"},
		{"Liberal","Winston Churchill"},
		{"Liberal","Franklin D. Roosevelt"},
		{"Liberal","Charles de Gaulle"},
		{"Liberal","Dwight Eisenhower"},
		{"Liberal","Harry S. Truman"},
		{"Liberal","Anne Frank"},
		};

		List <string> political_profiles;		
	Random random;

	public Button reveal_player;
	public Button next_player;


	[Header("Health Settings")]
	public Sprite facist_mem;
	public Sprite liberal_mem;

	public GameObject mem_card;
	public GameObject mem_card_placeholder;
	public GameObject profile_card;

	[Header("Text Object")]
	public TMP_Text player_name_display;

	public TMP_Text player_role_display;

	public TMP_Text [] awnsers;

	public TMP_Text player_title;

	public TMP_ColorGradient facist_gradient;
	public TMP_ColorGradient liberal_gradient;

	public TMP_ColorGradient defualt_gradient;

	

	[Header("Profile Pictures")]
	public Sprite [] profile_picures;

	byte player_no_2_displayer;


	public Sprite pressed;
	public Sprite not_pressed;

	GameObject [] button_txt;

	

	

	void Awake () {
		//test =  Resources.Load("Assets/Pictures/facist_member") as Texture2D;


		button_txt = GameObject.FindGameObjectsWithTag("button_txt");


		mem_card.SetActive (false);
		profile_card.SetActive(false);

		player_title.colorGradientPreset = defualt_gradient;

		mem_card_placeholder.GetComponent<Image>().sprite = liberal_mem;

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
		
		button_txt[1].transform.localPosition -= new Vector3 (0,5,0);
		next_player.GetComponent<Button>().image.sprite  = pressed;
		next_player.GetComponentInChildren<TMP_Text> ().color -= new Color (0,0,0,0.5f);
		next_player.interactable = false;
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

		reveal_player.interactable = false;
		next_player.interactable = true;

		button_txt[1].transform.localPosition += new Vector3 (0,5,0);
		button_txt[0].transform.localPosition -= new Vector3 (0,5,0);

		Debug.Log(next_player.GetComponent<Image>().color);

	
		//reveal_player.GetComponent<Image>().color -= new Color (0,0,0,0.5f);
		//next_player.GetComponent<Image>().color += new Color (0,0,0,0.5f);

		reveal_player.GetComponent<Button>().image.sprite = pressed;
		next_player.GetComponent<Button>().image.sprite  = not_pressed;

		reveal_player.GetComponentInChildren<TMP_Text> ().color -= new Color (0,0,0,0.5f);
		next_player.GetComponentInChildren<TMP_Text> ().color += new Color (0,0,0,0.5f);

		assign_profile_picture (GameLogic.players[player_no_2_displayer].profile_card);

		mem_card.SetActive (true);
		profile_card.SetActive(true);

		player_role_display.text = GameLogic.players[player_no_2_displayer].profile_card + " " + "(" +  GameLogic.players[player_no_2_displayer].membership_card + ")";

		if (GameLogic.players[player_no_2_displayer].membership_card == "Facist") {
			mem_card.GetComponent<Image>().sprite = facist_mem;
			mem_card_placeholder.GetComponent<Image>().sprite = facist_mem;
			player_title.colorGradientPreset = facist_gradient;
		} else
		{
			mem_card.GetComponent<Image>().sprite = liberal_mem;
			mem_card_placeholder.GetComponent<Image>().sprite = liberal_mem;
			player_title.colorGradientPreset = liberal_gradient;
		}
	}

	public void next_player_2_reveal () {

		button_txt[1].transform.localPosition -= new Vector3 (0,5,0);
		button_txt[0].transform.localPosition += new Vector3 (0,5,0);



		//next_player.GetComponent<Image>().color -= new Color (0,0,0,0.5f);
		//reveal_player.GetComponent<Image>().color += new Color (0,0,0,0.5f);

		reveal_player.GetComponentInChildren<TMP_Text> ().color += new Color (0,0,0,0.5f);
		next_player.GetComponentInChildren<TMP_Text> ().color -= new Color (0,0,0,0.5f);

		reveal_player.GetComponent<Button>().image.sprite  = not_pressed;
		next_player.GetComponent<Button>().image.sprite  = pressed;

		reveal_player.interactable = true;
		next_player.interactable = false;

		player_title.colorGradientPreset = defualt_gradient;
		mem_card_placeholder.GetComponent<Image>().sprite = liberal_mem;
		mem_card.SetActive (false);
		profile_card.SetActive(false);
		player_no_2_displayer ++;

				awnsers[0].text = "";
				awnsers[1].text = "";
				awnsers[2].text = "";
				awnsers[3].text = "";

		if (player_no_2_displayer < GameLogic.players.Count) {
			player_role_display.text = " ";
			player_name_display.text = GameLogic.players[player_no_2_displayer].user_name;
		} else {
			SceneManager.LoadScene ("Board");
		}
	}


	public void temp_funtion () {
		SceneManager.LoadScene ("Board");
	}

	void assign_profile_picture (string name) {

		Image tmp_image = profile_card.GetComponent<Image> ();
		switch (name)
		{
			case ("Adolf Hitler"):
				tmp_image.sprite = profile_picures[0];
				awnsers[0].text = "April 20, 1889";
				awnsers[1].text = "German";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Aspirering artist";

				break;

			case ("Joseph Goebbels"):
				tmp_image.sprite = profile_picures[1];
				awnsers[0].text = "Oktober 29, 1897";
				awnsers[1].text = "German";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Likes alternative News";
				break;

			case ("Heinrich Himmler"):
				tmp_image.sprite = profile_picures[2];
				awnsers[0].text = "Oktober 7, 1900";
				awnsers[1].text = "German";
				awnsers[2].text = "Reichsführer";
				awnsers[3].text = "Likes to backpack";
				break;

			case ("Hermann W. Göring"):
				tmp_image.sprite = profile_picures[3];
				awnsers[0].text = "January 12, 1893";
				awnsers[1].text = "German";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Cyanide entusiast";
				break;

			case ("Winston Churchill"):
				tmp_image.sprite = profile_picures[4];
				awnsers[0].text = "November 30, 1874";
				awnsers[1].text = "British";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Anti-prohabionist";
				break;


			case ("Franklin D. Roosevelt"):
				tmp_image.sprite = profile_picures[5];
				awnsers[0].text = "January 30, 1882";
				awnsers[1].text = "American";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Likes rolling along the beach";
				break;


			case ("Charles de Gaulle"):
				tmp_image.sprite = profile_picures[6];
				awnsers[0].text = "November 22, 1890 ";
				awnsers[1].text = "French";
				awnsers[2].text = "Army officer";
				awnsers[3].text = "French stuff";
				break;

			case ("Dwight Eisenhower"):
				tmp_image.sprite = profile_picures[7];
				awnsers[0].text = "October 14, 1890";
				awnsers[1].text = "American";
				awnsers[2].text = "Army general";
				awnsers[3].text = "Likes painting";
				break;

			case ("Harry S. Truman"):
				tmp_image.sprite = profile_picures[8];
				awnsers[0].text = "May 8, 1884 ";
				awnsers[1].text = "American";
				awnsers[2].text = "Politician";
				awnsers[3].text = "Pyromaniac";
				break;

			case ("Anne Frank"):
				tmp_image.sprite = profile_picures[9];
				awnsers[0].text = "June 12, 1929";
				awnsers[1].text = "German";
				awnsers[2].text = "Student";
				awnsers[3].text = "shhh...";
				break;

			default:
				break;

		}
	}

}
