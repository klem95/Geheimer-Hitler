using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdown_side : MonoBehaviour {


	public float speed;
	public GameObject target_in;
	public GameObject target_out;

	public Button draw_card_but;

	public Button show_board_but;

	public Button [] interface_buttons;



	// Use this for initialization
	void Start () {

		Dealer.legislation_played += activate_dropdown_down;
		

		draw_card_but.onClick.AddListener(activate_dropdown_up);

		show_board_but.onClick.AddListener(activate_dropdown_down);

		

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void activate_dropdown_up () {
		StartCoroutine ("animating_dropdown_up");

	}

	IEnumerator animating_dropdown_up () {

		while (Mathf.Round(transform.localPosition.x) > target_in.transform.localPosition.x) {
			float step = speed * Time.deltaTime;
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, target_in.transform.localPosition, step);
			//Debug.Log("my: " + Mathf.Round(transform.position.y) );
			//Debug.Log("target: " + Mathf.Round(target_up.transform.position.y) );
			yield return new WaitForSeconds (0.01f);

			
		}

	
		
	}

	public void activate_dropdown_down () {
		
		StartCoroutine ("animating_dropdown_down");

	}

	IEnumerator animating_dropdown_down () {


		while (transform.localPosition.x < target_out.transform.localPosition.x) {
			float step = speed * Time.deltaTime;
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, target_out.transform.localPosition, step);
			yield return new WaitForSeconds (0.01f);
		}	
		
	}
}
