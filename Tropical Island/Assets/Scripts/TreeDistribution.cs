using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Class for generating the first tree distribution
/// </summary>
public class TreeDistribution : MonoBehaviour {

	public GameObject tree;
	public int nrOfPlants = 25;
	public Slider plantSlider;    //Slider to change the nr of plants in scene
	public bool linearGrid = false;
	public Button startSimButton;
	public Button stopSimButton;
	public Button generate3DButton;
	public bool randomizeRadius = false;

	private Renderer rend;
	private float xMin, xMax, yMin, yMax;     //The corner points on where the plants will be distributed
	private float width, height;              //Width and height of the plane/grid
	private float r;                          //Radius of the plant
	private int nrOfRows, nrOfCols;
	private List<GameObject> plants;
	private TreeGrowthSimulation simulator;
	private bool simulationRunning = false;
	private float scaling = 3f;							//How much the radius of the trees will be scaled if randomizeRadius is active

	void Start () {
		simulator = GetComponent<TreeGrowthSimulation>();

		startSimButton.onClick.AddListener(StartSimulation);
		startSimButton.gameObject.GetComponentInChildren<Text>().text = "Start Simulation";
		stopSimButton.onClick.AddListener(StopSimulation);
		stopSimButton.gameObject.GetComponentInChildren<Text>().text = "Stop Simulation";
		generate3DButton.onClick.AddListener(Generate3D);
		generate3DButton.gameObject.GetComponentInChildren<Text>().text = "Generate 3D";
		
		plants = new List<GameObject>();

		rend = GetComponent<Renderer>();
		Bounds bounds = rend.bounds;
		xMin = bounds.min.x;
		xMax = bounds.max.x;
		yMin = bounds.min.y;
		yMax = bounds.max.y;

		width = xMax - xMin;
		height = yMax - yMin;

		r = tree.GetComponent<Renderer>().bounds.extents.magnitude;
		//Debug.Log(r);
		InitDistribution();

	}

	void StartSimulation()
	{
		simulator.StartSimulation(plants);
		plantSlider.interactable = false;
		simulationRunning = true;
	}

	void StopSimulation()
	{
		simulator.StopSimulation();
		plantSlider.interactable = true;
		simulationRunning = false;
	}

	void Generate3D()
	{
		GameObject generator = GameObject.Find("3DGenerator");
		generator.GetComponent<Generate3DScene>().StartGereration(plants);
	}
	void Update()
	{
		if (!simulationRunning)
		{
			int newNrOfPlants = plantSlider.GetComponent<PlantsSliderScript>().Value;
			if (newNrOfPlants != nrOfPlants)
			{
				DeletePlants();
				nrOfPlants = newNrOfPlants;
				InitDistribution();
			}
		}
	}
	/// <summary>
	/// Call this if the nr of plants has changed
	/// </summary>
	public void InitDistribution()
	{
		nrOfCols = (int)Mathf.Sqrt(nrOfPlants);
		nrOfRows = nrOfPlants / nrOfCols;

		CreateScatter(xMin, yMin, nrOfRows + 1, nrOfCols + 1);
	}

	/// <summary>
	/// Use for gerating the orignal tree distribution
	/// </summary>
	/// <param name="x">Start x position</param>
	/// <param name="y">Start y position</param>
	/// <param name="rows">Nr of rows in grid</param>
	/// <param name="cols">Nr of columns in grid</param>
	void CreateScatter(float x, float y, int rows, int cols)
	{
		float dx = width / cols;
		float dy = height / rows;

		for(int i = 1; i < cols; i++)
		{
			for(int j = 1; j < rows; j++)
			{
				float xPos, yPos;

				if (linearGrid)
				{
					xPos = x + dx * i;
					yPos = y + dy * j;
				}
				else
				{
					xPos = x + dx * i + (dx / 2) * Random.Range(-1f, 1f) - r;       //remove r to avoid collsion
					yPos = y + dy * j + (dy / 2) * Random.Range(-1f, 1f) - r;
				}

				SpawnTree(xPos, yPos);
			}
		}
	}

	/// <summary>
	/// Creates the tree at the specified position
	/// </summary>
	/// <param name="x">x-coordinate</param>
	/// <param name="y">y-coordinate</param>
	void SpawnTree(float x, float y)
	{
		if (randomizeRadius)
		{
			float radiusAdjustment = Random.Range(-scaling * r, scaling * r);
			Vector3 oldScale = tree.transform.localScale;
			Vector3 newScale = new Vector3(oldScale.x + radiusAdjustment, oldScale.y + radiusAdjustment, oldScale.z);

			tree.GetComponent<PlantScript>().type = (Random.Range(0f, 1f) > 0.5f) ? PlantScript.PlantType.Tree1 : PlantScript.PlantType.Tree2;

			plants.Add(Instantiate(tree, new Vector3(x, y, -1f), Quaternion.identity) as GameObject);
			plants[plants.Count - 1].transform.localScale = newScale;
		}
		else
		{
			plants.Add(Instantiate(tree, new Vector3(x, y, -1f), Quaternion.identity) as GameObject);
		}

		plants[plants.Count - 1].GetComponent<PlantScript>().ChangeColor();
	}
	/// <summary>
	/// 
	/// </summary>
	void DeletePlants()
	{
		foreach(GameObject plant in plants)
		{
			Destroy(plant);
		}
		plants.Clear();

	}
}
