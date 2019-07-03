using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VariableList : MonoBehaviour {

    //object to hold variables

    public Dictionary<string, string> variables; // Dictionary holds <variableName, variableVlaue>

    // Use this for initialization
    void Start () {

        variables = new Dictionary<string, string>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
