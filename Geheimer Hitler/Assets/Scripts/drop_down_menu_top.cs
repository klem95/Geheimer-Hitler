using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drop_down_menu_top : MonoBehaviour {

	public float speed;
	public GameObject target_up;
	public GameObject target_down;

	public Button [] dropdown_buttons;

	public Button close_but;

	public Button [] vote_buttons;

	public static bool remote_activation;

	

	// Use this for initialization
	void Start () {

		goverment_manager.election_outcome += activate_dropdown_down;

		foreach (Button button in dropdown_buttons)
		{
			if (button.name != "Draw") {
				button.onClick.AddListener(activate_dropdown_up);
			}
			
		}

		close_but.onClick.AddListener(activate_dropdown_down);

	
		
	}
	
	// Update is called once per frame
	void Update () {

		if (remote_activation) {
			foreach (Button obj in vote_buttons)
			{
				if (obj.name != "Draw") {
					obj.onClick.AddListener(activate_dropdown_down);
				}
			}

			remote_activation = false;
		}
		
	}


	public void activate_dropdown_up () {

		foreach (Button button in dropdown_buttons)
		{
			button.interactable = false;
		}
		
		StartCoroutine ("animating_dropdown_up");

	}

	IEnumerator animating_dropdown_up () {

		while (Mathf.Round(transform.position.y) > Mathf.Round(target_up.transform.position.y)) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target_up.transform.position, step);
			//Debug.Log("my: " + Mathf.Round(transform.position.y) );
			//Debug.Log("target: " + Mathf.Round(target_up.transform.position.y) );
			yield return new WaitForSeconds (0.01f);

			
		}

	
		
	}

	public void activate_dropdown_down () {
		
		StartCoroutine ("animating_dropdown_down");

	}

	IEnumerator animating_dropdown_down () {

		Debug.Log("play");

		while (transform.position.y < target_down.transform.position.y) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target_down.transform.position, step);
			yield return new WaitForSeconds (0.01f);
		}

			foreach (Button button in dropdown_buttons)
		{
			button.interactable = true;
		}
	
		
	}
}
