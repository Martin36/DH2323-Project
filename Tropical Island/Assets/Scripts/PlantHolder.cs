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
	public GameObject terrain1;
	public GameObject terrain2;
	public GameObject[] selectedPlants;
	public GameObject selectedTerrain;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void SaveData(GameObject[] plants, GameObject terrain)
	{
		selectedPlants = plants.Clone() as GameObject[];
		selectedTerrain = terrain;
		DontDestroyOnLoad(selectedTerrain);
	}
}
