using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class for making the growth simulation of the plants
/// </summary>
public class TreeGrowthSimulation : MonoBehaviour {

	private List<GameObject> plants;
	private bool simulationOn = false;
	private float maxRadius = 100f;
	private float growthSpeed = 10f;

	void Start () {
		
	}
	
	public void StartSimulation(List<GameObject> plants)
	{
		this.plants = plants;
		simulationOn = true;
	}

	public void StopSimulation()
	{
		simulationOn = false;
	}

	void Update () {
		if (simulationOn)
		{
			for(int i = plants.Count-1; i >= 0; i--)
			{
				GameObject plant = plants[i];
				//Domination check is done by colliders
				if (plant.GetComponent<CollisionHandler>().IsDominated)
				{
					plants.RemoveAt(i);
					Destroy(plant);
				}

				//Check if radius has not reached its maximum
				else if(plant.GetComponent<CircleCollider2D>().radius <= maxRadius)
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
		float radiusIncrease = growthSpeed * Time.deltaTime;

		Vector3 oldLocalScale = plant.transform.localScale;
		Vector3 newLocalScale = new Vector3(oldLocalScale.x + radiusIncrease, oldLocalScale.y + radiusIncrease, oldLocalScale.z);
		plant.transform.localScale = newLocalScale;


		//Debug.Log(plant.transform.localScale.x / plant.GetComponent<CircleCollider2D>().radius);
	}
}
