using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dropdown_menu : MonoBehaviour {

	public float speed;
	public GameObject target_up;
	public GameObject target_down;


	public Button dropdown_but;

	string prew_scene;

	public GameObject [] dropdown_menu_but;

	void Awake() {
        DontDestroyOnLoad (gameObject);
    }

	// Use this for initialization
	void Start () {
	
		dropdown_menu_but = GameObject.FindGameObjectsWithTag("dropdown_buttons");
		//Debug.Log(SceneManager.GetActiveScene().name);
		dropdown_but.onClick.AddListener(activate_dropdown_up);

		load_dropdown (SceneManager.GetActiveScene().name);
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void load_dropdown (string _scene) {
		if (_scene == "Player_registration") {
			prew_scene = "Start_screen";

			foreach (GameObject obj in dropdown_menu_but)
			{
				if (obj.name == "Info"){
					obj.GetComponent<Button>().interactable = false;
					//obj.GetComponent<Image>().color -= new Color (0,0,0,0.7f);
				} else if (obj.name == "Settings") {
					obj.GetComponent<Button>().interactable = false;
					//obj.GetComponent<Image>().color -= new Color (0,0,0,0.7f);
				} else {
					obj.GetComponent<Button> ().onClick.AddListener(scene_dir);
				}
			}
		} else if (_scene == "Role_delegation") {
			prew_scene = "Player_registration";
			foreach (GameObject obj in dropdown_menu_but)
			{
				if (obj.name == "Info"){
					
				} else if (obj.name == "Settings") {
					
				} else {
					obj.GetComponent<Button> ().onClick.AddListener(scene_dir);
				}
			}

		} else if (_scene == "Board") {
			prew_scene = "Role_delegation";

			foreach (GameObject obj in dropdown_menu_but)
			{
				if (obj.name == "Info"){
					
				} else if (obj.name == "Settings") {
					
				} else {
					obj.GetComponent<Button> ().onClick.AddListener(scene_dir);
				}
			}
		}
	}

	public void activate_dropdown_up () {
		dropdown_but.onClick.RemoveAllListeners();
		StartCoroutine ("animating_dropdown_up");

	}

	IEnumerator animating_dropdown_up () {

		while (Mathf.Round(transform.position.y) < Mathf.Round(target_up.transform.position.y)) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target_up.transform.position, step);
			//Debug.Log("my: " + Mathf.Round(transform.position.y) );
			//Debug.Log("target: " + Mathf.Round(target_up.transform.position.y) );
			yield return new WaitForSeconds (0.01f);

			
		}
		Debug.Log("play_0");
		dropdown_but.onClick.AddListener(activate_dropdown_down);
		
	}

	public void activate_dropdown_down () {
		dropdown_but.onClick.RemoveAllListeners();
		StartCoroutine ("animating_dropdown_down");

	}

	IEnumerator animating_dropdown_down () {

		Debug.Log("play");

		while (transform.position.y > target_down.transform.position.y) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target_down.transform.position, step);
			yield return new WaitForSeconds (0.01f);
		}
		dropdown_but.onClick.AddListener(activate_dropdown_up);
	}

	void scene_dir () {
		SceneManager.LoadScene(prew_scene);
	}

	
}
