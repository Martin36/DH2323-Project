using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float radius = GetComponent<Renderer>().bounds.extents.magnitude;
		Debug.Log(radius);
		Debug.Log(GetComponent<Renderer>().bounds.center.ToString());
		Debug.Log(GetComponent<Renderer>().bounds.max.ToString());
		Debug.Log(transform.localScale);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
