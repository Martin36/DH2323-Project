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
	public Slider plantSlider;		//Slider to change the nr of plants in scene

	private Renderer rend;
	private float xMin, xMax, yMin, yMax;     //The corner points on where the plants will be distributed
	private float width, height;              //Width and height of the plane/grid
	private float r;                          //Radius of the plant
	private int nrOfRows, nrOfCols;
	private List<GameObject> plants;


	void Start () {
		plantSlider.onValueChanged.AddListener(delegate { OnSliderChange(); });
		plants = new List<GameObject>();

		rend = GetComponent<Renderer>();
		Bounds bounds = rend.bounds;
		xMin = bounds.min.x;
		xMax = bounds.max.x;
		yMin = bounds.min.y;
		yMax = bounds.max.y;

		width = xMax - xMin;
		height = yMax - yMin;

		r = tree.GetComponent<Renderer>().bounds.max.x - tree.GetComponent<Renderer>().bounds.min.x;    //radius is max - min coordinate

		InitDistribution();
	}

	void OnSliderChange()
	{
		DeletePlants();
		nrOfPlants = (int) plantSlider.value;
		InitDistribution();
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
				float xPos = x + dx * i + (dx / 2) * Random.Range(-1f, 1f) - r;				//remove r to avoid collsion
				float yPos = y + dy * j + (dy / 2) * Random.Range(-1f, 1f) - r;

				//float xPos = x + dx * i;
				//float yPos = y + dy * j;

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
		plants.Add(Instantiate(tree, new Vector3(x, y, 0.1f), Quaternion.identity) as GameObject);
	}

	void DeletePlants()
	{
		foreach(GameObject plant in plants)
		{
			Destroy(plant);
		}
		plants.Clear();

	}
}
