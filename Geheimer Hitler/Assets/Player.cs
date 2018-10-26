using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	
	int ID;
	public string user_name, membership_card, profile_card; 
	public bool is_alive, locked_in;

	public Player (int _ID) {
		ID = _ID;

		locked_in = false;
	}
}