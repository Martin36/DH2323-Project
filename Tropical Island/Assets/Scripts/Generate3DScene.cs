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
    public Terrain terrain;

	private List<GameObject> plants3D;
	private List<GameObject> plants2D;
    private bool useTerrain;
    private float xSizeTerrain, ySizeTerrian, xSize2D, ySize2D;

    void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void StartGereration(List<GameObject> plants)
	{
        xSizeTerrain = terrain.terrainData.size.x;
        ySizeTerrian = terrain.terrainData.size.z;
        GameObject background = GameObject.FindGameObjectWithTag("GameController");
        xSize2D = background.GetComponent<Renderer>().bounds.size.x;
        ySize2D = background.GetComponent<Renderer>().bounds.size.y;

        plants2D = plants;
		foreach(GameObject plant in plants2D)
		{
			DontDestroyOnLoad(plant);
		}
        if(terrain)
        {
            useTerrain = true;
        }
        if (useTerrain)
        {
            SceneManager.LoadScene("Snowy_Mountain");
        }
        else
        {
            SceneManager.LoadScene("3D_Generation");
        }

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
                    if (useTerrain)
                    {
                        float height = plant.GetComponent<PlantScript>().spawnHeight;
                        //position = new Vector3(plant.transform.position.x, height, plant.transform.position.y);
                        position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
                    }
                    else
                    {
                        position = new Vector3(plant.transform.position.x, 4f, plant.transform.position.y);
                    }
                    plants3D.Add(Instantiate(tree1, position, Quaternion.identity) as GameObject);
					break;
				case PlantScript.PlantType.Tree2:
                    if (useTerrain)
                    {
                        float height = plant.GetComponent<PlantScript>().spawnHeight;
                        //position = new Vector3(plant.transform.position.x, height, plant.transform.position.y);
                        position = ConvertToTerrainCoordinates(plant.transform.position.x, plant.transform.position.y, height);
                    }
                    else
                    {
                        position = new Vector3(plant.transform.position.x, 2f, plant.transform.position.y);
                    }
                    plants3D.Add(Instantiate(tree2, position, Quaternion.identity) as GameObject);
					break;
			}
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

    Vector3 ConvertToTerrainCoordinates(float x, float y, float height)
    {
        float xTerrain = (xSizeTerrain / xSize2D) * x;
        float yTerrain = (ySizeTerrian / ySize2D) * y;
        return new Vector3(xTerrain, height, yTerrain);
    }
}
