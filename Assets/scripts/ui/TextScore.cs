using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class TextScore : MonoBehaviour {
	Text scoreText;

	void Start () {
		scoreText = GetComponent<Text>();
	}

	void Update () {
		scoreText.text = "Score: " + Manager.score;
	}
}
