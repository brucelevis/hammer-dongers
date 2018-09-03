using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColors {

	public static List<List<Color32>> Palettes = new List<List<Color32>> () {
		//blue
		new List<Color32>() {
			new Color32(146, 232, 192, 255),
			new Color32(79, 164, 184, 255),
			new Color32(76, 104, 133, 255),
			new Color32(58, 63, 94, 255)
		},
		//red
		new List<Color32>() {
			new Color32(255, 137, 51, 255),
			new Color32(230, 69, 57, 255),
			new Color32(173, 47, 69, 255),
			new Color32(120, 29, 79, 255)
		},
		//green
		new List<Color32>() {
			new Color32(200, 212, 93, 255),
			new Color32(99, 171, 63, 255),
			new Color32(59, 125, 79, 255),
			new Color32(47, 87, 83, 255)
		},
		//pink
		new List<Color32>() {
			new Color32(255, 194, 161, 255),
			new Color32(255, 82, 119, 255),
			new Color32(204, 47, 123, 255),
			new Color32(156, 42, 112, 255)
		},
	};

	public static List<Color32> GetPalette(int index) {
		return Palettes [index];
	}
}