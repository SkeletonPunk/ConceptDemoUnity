using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	[SerializeField]
	Color ColorA;
	[SerializeField]
	Color ColorB;
	[SerializeField]
	Color ColorC;

	public static uint score;
	public enum COLORS {COLORA, COLORB, COLORC}
	public static System.Action OnColorUpdate;

	public struct GameColors {
		static Color a;
		static Color b;
		static Color c;

		public static Color ColorA {
			get {
				return a;
			}
			set {
				a = value;
				if (OnColorUpdate != null) OnColorUpdate();
			}
		}
		public static Color ColorB {
			get {
				return b;
			}
			set {
				b = value;
				if (OnColorUpdate != null) OnColorUpdate();
			}
		}
		public static Color ColorC {
			get {
				return c;
			}
			set {
				c = value;
				if (OnColorUpdate != null) OnColorUpdate();
			}
		}

		public static Manager.COLORS RandomColor {
			get {
				Manager.COLORS[] colorList = {Manager.COLORS.COLORA, Manager.COLORS.COLORB, Manager.COLORS.COLORC};
				return colorList[Random.Range((int)0, (int)3)];
			}
		}
	}

	void Start() {
		GameColors.ColorA = ColorA;
		GameColors.ColorB = ColorB;
		GameColors.ColorC = ColorC;
	}

	public static Color GetColor(COLORS color) {
		if (color == COLORS.COLORA) return GameColors.ColorA;
		if (color == COLORS.COLORB) return GameColors.ColorB;
		if (color == COLORS.COLORC) return GameColors.ColorC;
		return GameColors.ColorA;
	}
}
