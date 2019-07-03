using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public string getVarName()
    {

        string name = "";

        name = transform.Find("VariableNameInputField").GetComponent<InputField>().text;

        return name;

    }

    public string getVarValue()
    {
        string value = "";

        value = transform.Find("VariableValueInputField").GetComponent<InputField>().text;

        return value;

    }


}
