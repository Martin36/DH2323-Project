﻿using UnityEngine;
using System.Collections;

public class PlantScript : MonoBehaviour {

	public enum PlantType{Tree1, Tree2};
	public PlantType type;
	public bool useShadeTolerance = true;

	private float shadeTolerance;   //Measurement of how likely the plan is to survive in shadow	
	private float oldAge;           //The probabillity that the plant dies when it has reached its maximum radius
	private float maxRadius;        //Maximal radius of the plant
	private float growthSpeed;
	private float radius;
	private Color color;            //Color of the plant
	private bool isDominated = false;
	private bool isDead = false;
	private bool useColor = true;
	


	void Awake() {
		switch (type)
		{
			case PlantType.Tree1:
				shadeTolerance = .995f;
				oldAge = .001f;
				maxRadius = 50f;
				color = Color.red;
				break;
			case PlantType.Tree2:
				shadeTolerance = .7f;
				oldAge = .0015f;
				maxRadius = 40f;
				color = Color.green;
				break;
		}
		growthSpeed = 20f;
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
			float randValue = Random.Range(0f, 1f);
			if(randValue < oldAge)
			{
				//Sad to say that the tree died of old age
				isDead = true;
			}
		}

	}

	public void ChangeColor()
	{

		if (!useColor)
		{
			float rgbValue = 1f - transform.GetComponent<Renderer>().bounds.extents.magnitude * (5.1f / 255f);
			Color newColor = new Color(rgbValue, rgbValue, rgbValue);
			GetComponent<SpriteRenderer>().color = newColor;
		}
		else
		{
			float radiusDifference = radius / maxRadius;
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
}
