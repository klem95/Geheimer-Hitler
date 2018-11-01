using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narative_manager {

	
	public Narative_manager () {

	}

	public string [] intro_message () {

		string [] string_2_send = new string [2];

		string_2_send[0] = "Chose your Chancellor";
		string_2_send[1] = "When chosen, the parliment has to vote by raise of hand. You as presedeint enters the result";

		return string_2_send;
	}

	public string [] show_drawn_cards () {
		string [] string_2_send = new string [4];

		string_2_send [0] = "President";
		string_2_send [1] = "You now have to pick 1 of these legislation to discard";
		string_2_send [2] = "Chancellor";
		string_2_send [3] = "You now have to pass one of the two following legislations";
		
		return string_2_send;

	}

	


}
