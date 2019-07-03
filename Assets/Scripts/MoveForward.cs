using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MoveForward : MonoBehaviour {

    public Component[] inputs;

    // Use this for initialization
    void Start () {

        inputs = GetComponentsInChildren<InputField>();


            

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getSpeed()
    {

        float spedOut = 0;
    
        foreach (InputField i in inputs)
        {

            if (i.name == "SpeedInput")
            {


                string input = this.GetComponentInChildren<InputField>().text;

                float.TryParse(input, out spedOut);

                print("Speed:" + input);

                return spedOut;

            }

        }

        return 0;

    }

    public float getDistance()
    {

        float distOut = 0;

        foreach (InputField j in inputs)
        {

            if(j.name == "DistanceInput")
            {


                string inputj = this.GetComponentInChildren<InputField>().text;

                float.TryParse(inputj, out distOut);

                print("Distance:" + inputj);

                return distOut;

            }

        }

        return 0;

    }
}
