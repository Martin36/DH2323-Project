using UnityEngine;
using System.Collections;
/// <summary>
/// Script for holding the plants
/// </summary>
public class PlantHolder : MonoBehaviour
{

	public GameObject plant1;
	public GameObject plant2;
	public GameObject plant3;
	public GameObject plant4;
	public GameObject plant5;
	public GameObject[] selectedPlants;
	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void SaveData(GameObject[] plants)
	{
		selectedPlants = plants.Clone() as GameObject[];
	}
}
