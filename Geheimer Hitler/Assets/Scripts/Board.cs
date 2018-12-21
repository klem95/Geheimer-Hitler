using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board  {

	public string board_size;

	public string [] power_ups_layout;

	public Board (string _board_size) {
		board_size = _board_size;
		power_ups_layout = setup_board (board_size);
	}

	string [] setup_board (string _size) {

		string [] temp_s = new string [6];

		if (_size == "small") {
			temp_s [0] = "none";
			temp_s [1] = "none";
			temp_s [2] = "draw_top";
			temp_s [3] = "execut";
			temp_s [4] = "execut";
		} else if (_size == "medium") {
			temp_s [0] = "none";
			temp_s [1] = "search";
			temp_s [2] = "elect";
			temp_s [3] = "execut";
			temp_s [4] = "execut";

		} else {
			temp_s [0] = "search";
			temp_s [1] = "search";
			temp_s [2] = "elect";
			temp_s [3] = "execut";
			temp_s [4] = "execut";
		}

		return temp_s;
	}

	public string check_4_power (int current_position) {
		return power_ups_layout[current_position];
	}





}
