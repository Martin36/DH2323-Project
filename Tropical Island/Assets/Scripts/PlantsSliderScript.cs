using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantsSliderScript : MonoBehaviour, IEndDragHandler
{

	private Slider slider;
	private int value;

	void Awake()
	{
		slider = gameObject.GetComponent<Slider>();
		value = (int) slider.value;
	}


	public void OnEndDrag(PointerEventData data)
	{
		value = (int) slider.value;
	}

	public int Value
	{
		get { return value; }
	}
}
