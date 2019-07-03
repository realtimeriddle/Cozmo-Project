using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportToWorkPanel : MonoBehaviour
{

    //If a block is put on the viewport it is moved to it's first child, which is
    //a workpanel

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach(Transform child in transform)
        {
            if(child.GetComponent<Block>() != null)
            {

                child.SetParent(this.transform.GetChild(0)); //set block to first child

            }

        }

    }
}
