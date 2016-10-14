using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class for making the growth simulation of the plants
/// </summary>
public class TreeGrowthSimulation : MonoBehaviour {

	private List<GameObject> plants;
	private bool simulationOn = false;
	private float maxRadius = 50f;
		
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
				Renderer rend = plant.GetComponent<Renderer>();
				//Domination check is done by colliders
				if (plant.GetComponent<PlantScript>().IsDead)
				{
					plants.RemoveAt(i);
					Destroy(plant);
				}
				else
				{
					plant.GetComponent<PlantScript>().Grow();
				}
			}
		}
	}
}
