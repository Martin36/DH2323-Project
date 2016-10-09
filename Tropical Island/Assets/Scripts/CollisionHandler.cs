using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	private bool isDominated = false;


	void OnCollisionEnter2D(Collision2D other)
	{
	//	Debug.Log("collision enter!");
		//check which of the objects is the largest
		float radius = GetComponent<CircleCollider2D>().radius;
		float otherRadius = 0;
		var circleColliderOther = other.transform.GetComponent<CircleCollider2D>();
		if (circleColliderOther != null)
			otherRadius = circleColliderOther.radius;

		if(radius <= otherRadius)
		{
			//			Destroy(gameObject);
			isDominated = true;
		}
		
	}

	public bool IsDominated
	{
		get { return isDominated; }
	}

}
