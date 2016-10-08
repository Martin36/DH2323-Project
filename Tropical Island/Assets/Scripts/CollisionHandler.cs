using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		//check which of the objects is the largest
		float radius = GetComponent<CircleCollider2D>().radius;
		float otherRadius = 0;
		var circleColliderOther = collider.transform.GetComponent<CircleCollider2D>();
		if (circleColliderOther != null)
			otherRadius = circleColliderOther.radius;

		if(radius <= otherRadius)
		{
			Destroy(gameObject);
		}

	}
}
