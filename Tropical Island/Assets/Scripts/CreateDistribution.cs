using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
/// <summary>
/// Script for jumping from the start screen to the 2D simulation scene
/// </summary>
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
		terrain = ph.terrain1;
		createDistButton = GetComponentInChildren<Button>();
	}
	/// <summary>
	/// This function is called when the "Create Distribution" button is pressed
	/// </summary>
	public void StartCreateDistribution()
	{
		ph.SaveData(plants, terrain);
		SceneManager.LoadScene("2D_Plant_Simulation");
	}
	/// <summary>
	/// This function initializes the dropdown lists for choosing the plants
	/// </summary>
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
		//When the plants has been assigned we want the Create Distribution to be interactable
		if (!createDistButton.interactable)
		{
			createDistButton.interactable = true;
		}
	}
	/// <summary>
	/// The function which is called when the user changes any of the values in the dropdowns.
	/// It sets that selected plant in the list of selected plants, in the right position.
	/// </summary>
	/// <param name="target">The Dropdown that has changed value</param>
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
	/// <summary>
	/// Called when the user make changes in terrain dropdown
	/// </summary>
	/// <param name="target"></param>
	public void ChangeTerrain(Dropdown target)
	{
		int value = target.value;
		switch (value)
		{
			case 0:
				terrain = ph.terrain1;
				break;
			case 1:
				terrain = ph.terrain2;
				break;
		}
	}

}
