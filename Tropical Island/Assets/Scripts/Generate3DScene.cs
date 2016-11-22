using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Script for generating the 3D environment from the 2D scene
/// </summary>
public class Generate3DScene : MonoBehaviour
{
	public GameObject tree1;
	public GameObject tree2;

	private GameObject[] trees;
	private Terrain terrain;
	private List<GameObject> plants3D;
	private List<GameObject> plants2D;
	private bool useTerrain;
	private bool useRotation = false;
	private bool useRandomYRotation = true;
	private float xSizeTerrain, ySizeTerrian, xSize2D, ySize2D;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		PlantHolder ph = GameObject.FindGameObjectWithTag("PlantHolder").GetComponent<PlantHolder>();
		terrain = ph.selectedTerrain.GetComponent<Terrain>();
		trees = ph.selectedPlants;
		useRotation = ph.useRotation;
	}

	public void StartGereration(List<GameObject> plants)
	{
		GameObject background = GameObject.FindGameObjectWithTag("GameController");
		//terrain = background.GetComponent<TreeDistribution>().terrain;
		xSizeTerrain = terrain.terrainData.size.x;
		ySizeTerrian = terrain.terrainData.size.z;
		xSize2D = background.GetComponent<Renderer>().bounds.size.x;
		ySize2D = background.GetComponent<Renderer>().bounds.size.y;

		plants2D = plants;
		foreach (GameObject plant in plants2D)
		{
			DontDestroyOnLoad(plant);
		}
		if (terrain.gameObject.tag.Equals("Island"))
				SceneManager.LoadScene("Snowy_Mountain");
		if (terrain.gameObject.tag.Equals("Mountain"))
				SceneManager.LoadScene("Terrain_Test");

		SceneManager.sceneLoaded += delegate { Loaded(); };

	}
	void Loaded()
	{
		PlantScript script;
		float maxRadius, radius, scaling;
		plants3D = new List<GameObject>();

		foreach (GameObject plant in plants2D)
		{
			script = plant.GetComponent<PlantScript>();
			maxRadius = script.MaxRadius;
			radius = script.Radius;
			scaling = radius / maxRadius;
			if (scaling < .2f)		//Skip adding the trees that are too small
				continue;
			Vector3 position;
			float height;
			GameObject plantObject;
			Quaternion rotation;

			if (useRandomYRotation)
			{
				rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
			}
			else
			{
				rotation = Quaternion.identity;
			}

			switch (script.type)
			{
				case PlantScript.PlantType.Tree1:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					//position = new Vector3(plant.transform.position.x, height, plant.transform.position.y);
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plants3D.Add(Instantiate(tree1, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.Tree2:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plants3D.Add(Instantiate(tree2, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.PalmDual:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plantObject = trees.Where(obj => obj.tag == "Palm_Dual").SingleOrDefault();		//Finds the object in the list with the specified tag
					plants3D.Add(Instantiate(plantObject, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.PalmDualBended:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plantObject = trees.Where(obj => obj.tag == "Palm_DualBended").SingleOrDefault();   
					plants3D.Add(Instantiate(plantObject, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.PalmSingle:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plantObject = trees.Where(obj => obj.tag == "Palm_Single").SingleOrDefault();
					plants3D.Add(Instantiate(plantObject, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.PalmSingleBended:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plantObject = trees.Where(obj => obj.tag == "Palm_SingleBended").SingleOrDefault();
					plants3D.Add(Instantiate(plantObject, position, rotation) as GameObject);
					break;
				case PlantScript.PlantType.PalmTrio:
					height = plant.GetComponent<PlantScript>().spawnHeight;
					position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
					plantObject = trees.Where(obj => obj.tag == "Palm_Trio").SingleOrDefault();
					plants3D.Add(Instantiate(plantObject, position, rotation) as GameObject);
					break;
			}
			Vector3 scalingVector = plants3D[plants3D.Count - 1].transform.localScale;
			scalingVector *= scaling;
			plants3D[plants3D.Count - 1].transform.localScale = scalingVector;

			if (useRotation)
			{
				Vector3 normal = plant.GetComponent<PlantScript>().spawnNormal;
			//rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

				/*
				Quaternion facing = plants3D[plants3D.Count - 1].transform.rotation;
				Quaternion rotation = Quaternion.LookRotation(normal);
				rotation *= facing;
				*/
				plants3D[plants3D.Count - 1].transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
			}
			Destroy2DPlants();
		}
	}

	void Destroy2DPlants()
	{
		foreach (GameObject plant in plants2D)
		{
			Destroy(plant);
		}
	}

	Vector3 ConvertToTerrainCoordinates(float x, float y, float height)
	{
		float xTerrain = (xSizeTerrain / xSize2D) * x;
		float yTerrain = (ySizeTerrian / ySize2D) * y;
		return new Vector3(xTerrain, height, yTerrain);
	}
}
