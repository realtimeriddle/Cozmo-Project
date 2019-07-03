using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showGroup()
    {

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

    }

    public void hideGroup()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }
}
