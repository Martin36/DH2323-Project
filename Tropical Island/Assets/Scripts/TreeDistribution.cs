using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Class for generating the first tree distribution
/// </summary>
public class TreeDistribution : MonoBehaviour {

	public GameObject tree;
    public Slider plantSlider;    //Slider to change the nr of plants in scene
    public Terrain terrain;
    public float minHeight = 3f;       //The min and max height that plants will grow on 
    public float maxHeight = 40f;
    public int nrOfPlants = 25;
	public bool linearGrid = false;
	public bool randomizeRadius = false;
    public bool useTerrain = true;  //True if the tree distribution should be limited to the given terrain

    private Renderer rend;
    private List<GameObject> plants;
    private TreeGrowthSimulation simulator;
    private float xMin, xMax, yMin, yMax;     //The corner points on where the plants will be distributed
	private float width, height;              //Width and height of the plane/grid
	private float r;                          //Radius of the plant
    private float scaling = 3f;                 //How much the radius of the trees will be scaled if randomizeRadius is active
    private int nrOfRows, nrOfCols;
	private bool simulationRunning = false;

    void Start () {
		simulator = GetComponent<TreeGrowthSimulation>();
		plants = new List<GameObject>();
        //If there is no terrain attached there will be no restriction
        if (useTerrain)
        {
            if(terrain == null)
            {
                useTerrain = false;
            }
        }
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

	public void StartSimulation()
	{
		simulator.StartSimulation(plants);
		plantSlider.interactable = false;
		simulationRunning = true;
	}

	public void StopSimulation()
	{
		simulator.StopSimulation();
		plantSlider.interactable = true;
		simulationRunning = false;
	}

	public void Generate3D()
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
                    SpawnTree(xPos, yPos);
                }
                else if (useTerrain)
                {
                    xPos = x + dx * i + (dx / 2) * Random.Range(-1f, 1f) - r;       //remove r to avoid collsion
                    yPos = y + dy * j + (dy / 2) * Random.Range(-1f, 1f) - r;
                    //float terrainHeight = terrain.SampleHeight(pos3d);'
                    float xNorm = NormalizedXCoordinate(xPos);
                    float yNorm = NormalizedYCoordinate(yPos);
                    float terrainHeight = terrain.terrainData.GetInterpolatedHeight(xNorm, yNorm);
                    if (terrainHeight > minHeight && terrainHeight < maxHeight)
                    {
                        SpawnTree(xPos, yPos, terrainHeight);
                    }
                }
                else
				{
					xPos = x + dx * i + (dx / 2) * Random.Range(-1f, 1f) - r;       //remove r to avoid collsion
					yPos = y + dy * j + (dy / 2) * Random.Range(-1f, 1f) - r;
                    SpawnTree(xPos, yPos);
                }

            }
		}
        //SpawnCorners();
    }

    void SpawnCorners()
    {
        float xPos, yPos, xNorm, yNorm, terrainHeight;
        //Top left corner
        xPos = xMin;
        yPos = yMin;    
        xNorm = NormalizedXCoordinate(xPos);
        yNorm = NormalizedYCoordinate(yPos);
        terrainHeight = terrain.terrainData.GetInterpolatedHeight(xNorm, yNorm);
        SpawnTree(xPos, yPos, terrainHeight);
        //Top right corner
        xPos = xMax;
        yPos = yMin;
        xNorm = NormalizedXCoordinate(xPos);
        yNorm = NormalizedYCoordinate(yPos);
        terrainHeight = terrain.terrainData.GetInterpolatedHeight(xNorm, yNorm);
        SpawnTree(xPos, yPos, terrainHeight);
        //Bottom right corner
        xPos = xMax;
        yPos = yMax;
        xNorm = NormalizedXCoordinate(xPos);
        yNorm = NormalizedYCoordinate(yPos);
        terrainHeight = terrain.terrainData.GetInterpolatedHeight(xNorm, yNorm);
        SpawnTree(xPos, yPos, terrainHeight);
        //Bottom left corner
        xPos = xMin;
        yPos = yMax;
        xNorm = NormalizedXCoordinate(xPos);
        yNorm = NormalizedYCoordinate(yPos);
        terrainHeight = terrain.terrainData.GetInterpolatedHeight(xNorm, yNorm);
        SpawnTree(xPos, yPos, terrainHeight);

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
    /// Creates the tree at the specified position with the height parameter for 3D generation on terrain
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="height"></param>
    void SpawnTree(float x, float y, float height)
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
        plants[plants.Count - 1].GetComponent<PlantScript>().spawnHeight = height;

    }

    /// <summary>
    /// Removes all the plants in the scene
    /// </summary>
    void DeletePlants()
	{
		foreach(GameObject plant in plants)
		{
			Destroy(plant);
		}
		plants.Clear();

	}
    /// <summary>
    /// Normalizes the x-coordinate to a value between 0 and 1
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public float NormalizedXCoordinate(float x)
    {
        float xNorm = (x - xMin) / Mathf.Abs(xMax - xMin);
        return xNorm;
    }
    /// <summary>
    /// Normalizes the y-coordinate to a value between 0 and 1
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public float NormalizedYCoordinate(float y)
    {
        float yNorm = (y - yMin) / Mathf.Abs(yMax - yMin);
        return yNorm;
    }

    public float MaxHeight
    {
        get { return maxHeight; }
    }

    public float MinHeight
    {
        get { return minHeight; }
    }
}
