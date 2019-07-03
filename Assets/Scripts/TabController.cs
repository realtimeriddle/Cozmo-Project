using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour {

    public Tab tab1;
    public Tab tab2;

    public TabGroup SideTabs1;
    public TabGroup SideTabs2;



	// Use this for initialization
	void Start () {

        tab1.GetComponent<Tab>();
        tab2.GetComponent<Tab>();

        SideTabs1.GetComponent<TabGroup>();
        SideTabs2.GetComponent<TabGroup>();

    }
	
    // Update is called once per frame
	void Update () {

        if (tab1.transform.GetSiblingIndex() != tab1.transform.parent.childCount - 1)
        {

            SideTabs1.showGroup();
            SideTabs2.hideGroup();

        }
        else if (tab2.transform.GetSiblingIndex() != tab2.transform.parent.childCount - 1)
        {

            SideTabs2.showGroup();
            SideTabs1.hideGroup();

        }


    }


}
