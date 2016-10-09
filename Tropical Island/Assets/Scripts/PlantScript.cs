using UnityEngine;
using System.Collections;

public class PlantScript : MonoBehaviour {

	private bool isDominated = false;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public bool IsDominated
	{
		get { return isDominated; }
		set { isDominated = value; }
	}

}
