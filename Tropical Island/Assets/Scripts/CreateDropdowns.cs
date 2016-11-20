using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateDropdowns : MonoBehaviour {

    public GameObject plantSelector;

    private InputField plantsNrField;
    private CreateDistribution cd;
    private GameObject[] plantLists;
    private int dropdownNr;     //Number of dropdown lists to spawn (equals the nr of plants the user wants to use)

    void Awake()
    {
        plantsNrField = GetComponentInChildren<InputField>();
        cd = GetComponent<CreateDistribution>();
    }

    public void OnFinished()
    {
        string nr = plantsNrField.text;
        dropdownNr = int.Parse(nr);
        dropdownNr = (dropdownNr > 5) ? 5 : dropdownNr;
        plantLists = new GameObject[dropdownNr];
        SpawnDropdowns();
    }

    void SpawnDropdowns()
    {
        float xPos, yPos;       //Spawning position for the first dropdown
        xPos = 300f;
        yPos = 300f;
        for (int i = 0; i < dropdownNr; i++)
        {
            plantLists[i] = Instantiate(plantSelector, gameObject.transform) as GameObject;
            plantLists[i].transform.localPosition = new Vector3(xPos, yPos);
            yPos -= 50f;
        }
        cd.InitDropdowns();      //Notify that the values of the dropdowns has been changed
    }

    public GameObject[] PlantLists
    {
        get { return plantLists; }
    }
}
