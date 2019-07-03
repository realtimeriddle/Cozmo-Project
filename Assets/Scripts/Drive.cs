using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour {

    public Component[] inputs;

    // Use this for initialization
    void Start()
    {

        inputs = GetComponentsInChildren<InputField>();




    }

    // Update is called once per frame
    void Update()
    {

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

            }

        }

        return spedOut;

    }

    public float getAcceleration()
    {

        float accelOut = 0;

        foreach (InputField i in inputs)
        {

            if (i.name == "AccelerationInput")
            {

                string input = this.GetComponentInChildren<InputField>().text;

                float.TryParse(input, out accelOut);

            }

        }

        return accelOut;

    }
}
