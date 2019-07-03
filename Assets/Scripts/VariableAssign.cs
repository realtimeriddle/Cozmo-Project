using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VariableAssign : MonoBehaviour {

    VariableList variableList;

    // Use this for initialization
    void Start () {

        transform.Find("VariableDropdown").GetComponent<Dropdown>().ClearOptions();



    }
	
	// Update is called once per frame
	void Update () {

        foreach(KeyValuePair<string,string> k in variableList.variables)
        {

            transform.Find("VariableDropdown").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() {text = k.Key });

        }
        
        transform.Find("VariableDropdown").GetComponent<Dropdown>().ClearOptions();

    }
}
