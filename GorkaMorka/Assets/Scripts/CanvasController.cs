using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public Slider slider;

	public float sliderValue;


	// Use this for initialization
	void Start () {

		foreach (Transform child in this.transform) {

			child.gameObject.SetActive (false);
		}

		sliderValue = slider.value;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
