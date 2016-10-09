using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	private bool isDominated = false;


	void OnCollisionEnter2D(Collision2D other)
	{
		//check which of the objects is the largest
		
		float radius = GetComponent<Renderer>().bounds.extents.magnitude;    //radius is max - min coordinate
		
		float otherRadius = other.transform.GetComponent<Renderer>().bounds.extents.magnitude;

		if(radius <= otherRadius)
		{
			isDominated = true;
		}
		
	}

	public bool IsDominated
	{
		get { return isDominated; }
	}

}
