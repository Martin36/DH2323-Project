using UnityEngine;
using System.Linq;


public class PlantScript : MonoBehaviour
{

	public enum PlantType { Tree1, Tree2, PalmDualBended, PalmDual, PalmSingleBended, PalmSingle, PalmTrio };
	public PlantType type;
	[HideInInspector]
	public Vector3 spawnNormal = new Vector3();
	[HideInInspector]
	public float spawnHeight = 0f;
	public bool useShadeTolerance = true;
	public bool spawningEnabled = true;

	private Color color;            //Color of the plant
	private PlantHolder ph;
	private float shadeTolerance;   //Measurement of how likely the plan is to survive in shadow	
	private float oldAge;           //The probabillity that the plant dies when it has reached its maximum radius
	private float maxRadius;        //Maximal radius of the plant
	private float growthSpeed;
	private float radius;
	private float spawnTimer = 0f;
	private float spawnTime;
	private bool isDominated = false;
	private bool isDead = false;
	private bool useColor = true;
	private bool spawnReady = false;

	void Awake()
	{
		/*
		ph = GameObject.FindGameObjectWithTag("PlantHolder").GetComponent<PlantHolder>();
		GameObject[] trees = ph.selectedPlants;
		GameObject currentTree;
		*/
		switch (type)
		{
			case PlantType.Tree1:
				shadeTolerance = .995f;
				oldAge = .002f;
				maxRadius = 50f;
				color = Color.red;
				growthSpeed = 20f;
				spawnTime = 5f;
				break;
			case PlantType.Tree2:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 40f;
				color = Color.green;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
			case PlantType.PalmDual:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 60f;
				color = Color.blue;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
			case PlantType.PalmDualBended:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 80f;
				color = Color.green;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
			case PlantType.PalmSingle:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 40f;
				color = Color.magenta;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
			case PlantType.PalmSingleBended:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 50f;
				color = Color.red;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
			case PlantType.PalmTrio:
				shadeTolerance = .997f;
				oldAge = .0015f;
				maxRadius = 90f;
				color = Color.yellow;
				growthSpeed = 10f;
				spawnTime = 6f;
				break;
		}
		radius = GetComponent<Renderer>().bounds.extents.magnitude;
	}

	public void Grow()
	{
		if (isDominated)
		{
			if (useShadeTolerance)
			{
				isDead = (Random.Range(0f, 1f) > shadeTolerance);
			}
			else
			{
				isDead = true;
			}
		}
		if (spawningEnabled)
		{
			if (spawnTimer > spawnTime && radius > 0.8 * maxRadius)   //Only spawn new plants when the plant has reached half of maximum radius
			{
				spawnReady = true;
				spawnTimer = 0f;
			}
			else
			{
				spawnTimer += Time.deltaTime;
			}
		}
		if (radius <= maxRadius)
		{
			float radiusIncrease = growthSpeed * Time.deltaTime;
			Vector3 oldLocalScale = transform.localScale;
			Vector3 newLocalScale = new Vector3(oldLocalScale.x + radiusIncrease, oldLocalScale.y + radiusIncrease, oldLocalScale.z);
			transform.localScale = newLocalScale;
			radius = GetComponent<Renderer>().bounds.extents.magnitude;
			ChangeColor();
		}
		else
		{
			float randValue = Random.Range(0f, 1f);
			if (randValue < oldAge)
			{
				//Sad to say that the tree died of old age
				isDead = true;
			}
		}

	}

	public void ChangeColor()
	{
		float radiusDifference = radius / maxRadius;
		if (!useColor)
		{
			Color newColor = Color.Lerp(Color.white, Color.black, radiusDifference);
			GetComponent<SpriteRenderer>().color = newColor;
		}
		else
		{
			Color newColor = Color.Lerp(Color.white, color, radiusDifference);
			GetComponent<SpriteRenderer>().color = newColor;
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//check which of the objects is the largest
		float radius = GetComponent<Renderer>().bounds.extents.magnitude;
		float otherRadius = other.transform.GetComponent<Renderer>().bounds.extents.magnitude;
		if (radius <= otherRadius)
		{
			isDominated = true;
		}
	}

	public bool IsDominated
	{
		get { return isDominated; }
	}

	public bool IsDead
	{
		get { return isDead; }
	}

	public float MaxRadius
	{
		get { return maxRadius; }
	}

	public float Radius
	{
		get { return radius; }
	}

	public bool SpawnReady
	{
		get { return spawnReady; }
	}

	public void Spawned()
	{
		spawnReady = false;
	}
}
