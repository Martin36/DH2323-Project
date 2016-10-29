using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class for making the growth simulation of the plants
/// </summary>
public class TreeGrowthSimulation : MonoBehaviour {

	private List<GameObject> plants;
	private bool simulationOn = false;
	private Bounds bounds;

	public void StartSimulation(List<GameObject> plants)
	{
		this.plants = plants;
		bounds = GetComponent<Renderer>().bounds;
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
				PlantScript script = plant.GetComponent<PlantScript>();
				//Domination check is done by colliders
				if (script.IsDead)
				{
					plants.RemoveAt(i);
					Destroy(plant);
				}
				else
				{
					script.Grow();
				}

				if(script.SpawnReady)
				{
					Spawn(plant);
					script.Spawned();
				}
			}

		}
	}

	void Spawn(GameObject plant)
	{
		PlantScript script = plant.GetComponent<PlantScript>();
		float radius = script.Radius;
		float maxRadius = script.MaxRadius;
		
		//Randomize a new start position close to the original plant
		Vector3 spawnPos = new Vector3(plant.transform.position.x, plant.transform.position.y, plant.transform.position.z);
		var candidatesX = new[] { Random.Range(-2 * maxRadius, -2 * radius), Random.Range(2 * radius, 2 * maxRadius) };
		var candidatesY = new[] { Random.Range(-2 * maxRadius, -2 * radius), Random.Range(2 * radius, 2 * maxRadius) };
		int indX = Random.Range(0, 2);
		int indY = Random.Range(0, 2);
		float dx = candidatesX[indX];
		float dy = candidatesY[indY];
		spawnPos += new Vector3(dx, dy, 0f);
		
		//Check bounds
		if(spawnPos.x > (bounds.extents.x - maxRadius))
		{
			spawnPos.x -= (Mathf.Abs(2 * dx) + maxRadius);
		}
		else if(spawnPos.x < -(bounds.extents.x - maxRadius))
		{
			spawnPos.x += (Mathf.Abs(2 * dx) + maxRadius);
		}
		if(spawnPos.y > (bounds.extents.y - maxRadius))
		{
			spawnPos.y -= (Mathf.Abs(2 * dy) + maxRadius);
		}
		else if(spawnPos.y < -(bounds.extents.y - maxRadius))
		{
			spawnPos.y += (Mathf.Abs(2 * dy) + maxRadius);
		}

		plants.Add(Instantiate(plant, spawnPos, Quaternion.identity) as GameObject);
		plants[plants.Count - 1].transform.localScale = new Vector3(10f, 10f);
	}

}
