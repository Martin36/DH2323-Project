using UnityEngine;
/// <summary>
/// Script for holding the plants between the scenes
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
	/// <summary>
	/// Called right before the scene changes so that the choises the user made are saved
	/// </summary>
	/// <param name="plants"></param>
	/// <param name="terrain"></param>
	public void SaveData(GameObject[] plants, GameObject terrain)
	{
		selectedPlants = plants.Clone() as GameObject[];
		selectedTerrain = terrain;
		DontDestroyOnLoad(selectedTerrain);
	}
	/// <summary>
	/// Is called by the toggle button Use Rotation and gives the information to the next scene
	/// </summary>
	/// <param name="on"></param>
	public void Rotation(bool on)
	{
		useRotation = on;
	}
}
