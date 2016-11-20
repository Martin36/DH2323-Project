using UnityEngine;
using System.Collections;

public class Plant3DScript : MonoBehaviour {

	private Color color;
	private float shadeTolerance;   //Measurement of how likely the plan is to survive in shadow	
	private float oldAge;           //The probabillity that the plant dies when it has reached its maximum radius
	private float maxRadius;        //Maximal radius of the plant
	private float growthSpeed;

	void Awake()
	{
		DontDestroyOnLoad(this);

		switch (tag)
		{
			case "Palm_DualBended":
				color = Color.blue;
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = GetComponent<Renderer>().bounds.extents.magnitude;
				growthSpeed = 20f;
				break;
			case "Palm_Dual":
				color = Color.green;
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = GetComponent<Renderer>().bounds.extents.magnitude;
				growthSpeed = 20f;
				break;
			case "Palm_SingleBended":
				color = Color.magenta;
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = GetComponent<Renderer>().bounds.extents.magnitude;
				growthSpeed = 20f;
				break;
			case "Palm_Single":
				color = Color.red;
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = GetComponent<Renderer>().bounds.extents.magnitude;
				growthSpeed = 20f;
				break;
			case "Palm_Trio":
				color = Color.yellow;
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = GetComponent<Renderer>().bounds.extents.magnitude;
				growthSpeed = 20f;
				break;
		}
	}

}
