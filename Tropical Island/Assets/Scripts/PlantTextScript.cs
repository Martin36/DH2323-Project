using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlantTextScript : MonoBehaviour {

	private Slider plantSlider;
	private Text text;

	// Use this for initialization
	void Start () {
		plantSlider = transform.parent.GetChild(0).gameObject.GetComponent<Slider>();
		plantSlider.onValueChanged.AddListener(delegate { OnSliderChange(); });
		text = GetComponent<Text>();
	}

	void OnSliderChange()
	{
		text.text = string.Format("Number of Plants: {0}", plantSlider.value);
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
