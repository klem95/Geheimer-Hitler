using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	public bool president;
	public bool chancellor;
	public byte ID;
	public string user_name, membership_card, profile_card; 
	public bool is_alive, locked_in;

	public Player (byte _ID) {
		ID = _ID;
		president = false;
		chancellor = false;

		locked_in = false;
	}
}