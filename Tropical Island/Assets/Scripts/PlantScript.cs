using UnityEngine;
using System.Collections;

public class PlantScript : MonoBehaviour {

	public enum PlantType{Tree1, Tree2};
	public PlantType type;

	private float shadeTolerance;   //Measurement of how likely the plan is to survive in shadow	
	private float oldAge;           //The probabillity that the plant dies when it has reached its maximum radius
	private float maxRadius;        //Maximal radius of the plant
	private float growthSpeed;
	private float radius;
	private Color color;            //Color of the plant
	private bool isDominated = false;
	private bool isDead = false;


	void Start() {
		switch (type)
		{
			case PlantType.Tree1:
				shadeTolerance = .5f;
				oldAge = .1f;
				maxRadius = 50f;
				color = Color.red;
				break;
			case PlantType.Tree2:
				shadeTolerance = .7f;
				oldAge = .7f;
				maxRadius = 40f;
				color = Color.green;
				break;
		}
		growthSpeed = 20f;
		radius = GetComponent<Renderer>().bounds.extents.magnitude;
	}

	// Update is called once per frame
	void Update() {

	}
	public void Grow()
	{
		if (isDominated)
		{
			isDead = true;
		}

		if(radius <= maxRadius)
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
			float randValue = Random.Range(0, 1);
			if(randValue < oldAge)
			{
				//Sad to say that the tree died of old age
				isDead = true;
			}
		}

	}

	public void ChangeColor()
	{
		float rgbValue = 1f - transform.GetComponent<Renderer>().bounds.extents.magnitude * (5.1f / 255f);
		Color newColor = new Color(rgbValue, rgbValue, rgbValue);
		GetComponent<SpriteRenderer>().color = newColor;
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
}
