using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Script for generating the 3D environment from the 2D scene
/// </summary>
public class Generate3DScene : MonoBehaviour {

	public GameObject tree1;
	public GameObject tree2;

	private List<GameObject> plants3D;
	private List<GameObject> plants2D;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void StartGereration(List<GameObject> plants)
	{
		plants2D = plants;
		foreach(GameObject plant in plants2D)
		{
			DontDestroyOnLoad(plant);
		}
		Application.LoadLevel("3D_Generation");
		SceneManager.sceneLoaded += delegate { Loaded(); } ;

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
			Vector3 position;
			switch (script.type)
			{
				case PlantScript.PlantType.Tree1:
					position = new Vector3(plant.transform.position.x, 4f, plant.transform.position.y);
					plants3D.Add(Instantiate(tree1, position, Quaternion.identity) as GameObject);
					break;
				case PlantScript.PlantType.Tree2:
					position = new Vector3(plant.transform.position.x, 2f, plant.transform.position.y);
					plants3D.Add(Instantiate(tree2, position, Quaternion.identity) as GameObject);
					break;
			}
//			DontDestroyOnLoad(plants3D[plants3D.Count - 1]);
			Vector3 scalingVector = plants3D[plants3D.Count - 1].transform.localScale;
			scalingVector *= scaling;
			plants3D[plants3D.Count - 1].transform.localScale = scalingVector;
			Destroy2DPlants();
		}
	}

	void Destroy2DPlants()
	{
		foreach(GameObject plant in plants2D)
		{
			Destroy(plant);
		}
	}

}
