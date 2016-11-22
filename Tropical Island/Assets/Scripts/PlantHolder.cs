using UnityEngine;
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
	[HideInInspector]
	public GameObject[] selectedPlants;
	[HideInInspector]
	public GameObject selectedTerrain;
	[HideInInspector]
	public bool useRotation;

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

	public void Rotation(bool on)
	{
		useRotation = on;
	}
}
