using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class SetLiftHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getHeight()
    {
        float height = 0;

        string input = this.GetComponentInChildren<InputField>().text;

        if (float.TryParse(input, out height))
        {

            return height;

        }

        return height;
    }
}
