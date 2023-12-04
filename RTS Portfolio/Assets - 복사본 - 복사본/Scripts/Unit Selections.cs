using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections instance;
    public static UnitSelections Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSelect(GameObject unit)
    {
        DeselectAll();
        unitsSelected.Add(unit);
        unit.transform.Find("Select Ring").gameObject.SetActive(true);
        unit.GetComponent<UnitMovement>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unit)
    {
        if(!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            unit.transform.Find("Select Ring").gameObject.SetActive(true);
            unit.GetComponent<UnitMovement>().enabled = true;
        }
        else
        {
            unitsSelected.Remove(unit);
            unit.transform.Find("Select Ring").gameObject.SetActive(false);
            unit.GetComponent<UnitMovement>().enabled = false;
        }
    }

    public void DragSelect(GameObject unit)
    {
        if(!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            unit.transform.Find("Select Ring").gameObject.SetActive(true);
            unit.GetComponent<UnitMovement>().enabled = true;
        }
    }

    public void DeselectAll()
    {
        foreach(GameObject unit in unitsSelected)
        {
            unit.GetComponent<UnitMovement>().enabled = false;
            unit.transform.Find("Select Ring").gameObject.SetActive(false);
        }

        unitsSelected.Clear();
    }

    public void Deselect(GameObject unit)
    {

    }
}
