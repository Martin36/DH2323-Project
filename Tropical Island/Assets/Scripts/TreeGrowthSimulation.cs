using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class for making the growth simulation of the plants
/// </summary>
public class TreeGrowthSimulation : MonoBehaviour {

	private List<GameObject> plants;
	public bool simulationOn = false;
	private float maxRadius = 100f;
	private float growthSpeed = 1;

	void Start () {
		
	}
	
	public void StartSimulation(List<GameObject> plants)
	{
		this.plants = plants;
		simulationOn = true;
	}

	void Update () {
		if (simulationOn)
		{
			foreach(GameObject plant in plants)
			{
				//Domination check is done by colliders
				//Check if radius has not reached its maximum
				if(plant.GetComponent<CircleCollider2D>().radius <= maxRadius)
				{
					//Then increase the size of the plant
					Grow(plant);
				}	
			}
		}
	}

	/// <summary>
	/// Increases the size of the plant
	/// </summary>
	/// <param name="plant"></param>
	void Grow(GameObject plant)
	{
		float currentRadius = plant.GetComponent<CircleCollider2D>().radius;
		float radiusIncrease = growthSpeed * Time.deltaTime;
		float procIncrease = (currentRadius + radiusIncrease) / currentRadius;		//The procuental increase of the radius, need for sprite scaling
		plant.GetComponent<CircleCollider2D>().radius += radiusIncrease;
		plant.transform.localScale = new Vector3(procIncrease, procIncrease, procIncrease);		//Scales the sprite
	}
}
