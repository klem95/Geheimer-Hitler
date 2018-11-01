using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log_data  {

	public string current_pres, current_chanc;
	public string card_for_pres, card_for_chanc;

	public string power_shooter, killed, membercard_shown;

	public string board_status_aft_play;

	public string veto_power_obtained, veto_power_blocked_members;

	public Log_data (string _current_pres,string _current_chanc) {
		this.current_pres = _current_pres;
		this.current_chanc = _current_chanc;
	}

	public void save_special_data_shot (string _power_shooter,string _killed) {
		this.power_shooter = _power_shooter;
		this.killed = _killed;

	}

	public void save_special_data_reveal (string _membercard_shown) {
		this.membercard_shown = _membercard_shown;

	}

}
