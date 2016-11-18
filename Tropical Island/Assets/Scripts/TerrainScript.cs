using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(GetComponent<Terrain>().terrainData.size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
