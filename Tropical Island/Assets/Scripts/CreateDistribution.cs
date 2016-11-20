using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CreateDistribution : MonoBehaviour
{

	[HideInInspector]
	public GameObject terrain;
	[HideInInspector]
	public GameObject[] plants;

	private CreateDropdowns cd;
	private PlantHolder ph;
	private Dropdown[] plantLists;
	private Button createDistButton;

	void Awake()
	{
		cd = GetComponent<CreateDropdowns>();
		ph = GameObject.Find("PlantHolder").GetComponent<PlantHolder>();
		createDistButton = GetComponentInChildren<Button>();
	}

	public void StartCreateDistribution()
	{
		ph.SaveData(plants);
		SceneManager.LoadScene("2D_Plant_Simulation");
	}
	public void InitDropdowns()
	{
		if (plantLists == null)
		{
			GameObject[] dropdowns = cd.PlantLists;
			plantLists = new Dropdown[dropdowns.Length];
			plants = new GameObject[dropdowns.Length];
			for (int i = 0; i < dropdowns.Length; i++)
			{
				Dropdown current = dropdowns[i].GetComponent<Dropdown>();
				current.onValueChanged.AddListener(delegate { ChangePlant(current); });
				plantLists[i] = current;
			}
		}
		UpdatePlants();

	}
	/// <summary>
	/// Sets the plants list to the current selected values
	/// </summary>
	void UpdatePlants()
	{
		for (int i = 0; i < plants.Length; i++)
		{
			int current = plantLists[i].value;
			switch (current)
			{
				case 0:
					plants[i] = ph.plant1;
					break;
				case 1:
					plants[i] = ph.plant2;
					break;
				case 2:
					plants[i] = ph.plant3;
					break;
				case 3:
					plants[i] = ph.plant4;
					break;
				case 4:
					plants[i] = ph.plant5;
					break;
			}
		}
		if (!createDistButton.interactable)
		{
			createDistButton.interactable = true;
		}
	}

	void ChangePlant(Dropdown target)
	{
		int value = target.value;
		int indx = Array.IndexOf(plantLists, target);
		switch (value)
		{
			case 0:
				plants[indx] = ph.plant1;
				break;
			case 1:
				plants[indx] = ph.plant2;
				break;
			case 2:
				plants[indx] = ph.plant3;
				break;
			case 3:
				plants[indx] = ph.plant4;
				break;
			case 4:
				plants[indx] = ph.plant5;
				break;
		}
		
	}
}
