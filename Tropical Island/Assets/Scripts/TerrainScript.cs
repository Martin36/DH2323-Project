using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class TerrainScript : MonoBehaviour {

	private float minHeight, maxHeight;   //The min and max height that plants will grow on  

	void Awake()
	{
		if(tag == "Island")
		{
			minHeight = 5;
			maxHeight = 50;
		}
		else if(tag == "Mountain")
		{
			minHeight = -1;
			maxHeight = 40;
		}
		else
		{
			minHeight = 0;
			maxHeight = 100;
		}
	}

	public float MinHeight
	{
		get { return minHeight; }
	}
	public float MaxHeight
	{
		get { return maxHeight; }
	}
}
