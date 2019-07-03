using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerClickHandler
{

	// Use this for initialization
	void Start () {

        //this.gameObject.SetActive(true);
        //this.transform.SetAsLastSibling();
		
	}	
	// Update is called once per frame
	void Update () {

        if(this.transform.GetSiblingIndex() != this.transform.parent.childCount - 1)
        {

            setTabUnactive();

        }

		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
        setTabActive();
    }

    public void setTabActive()
    {
        print(this.name + " is active");
        this.gameObject.SetActive(true);
        this.transform.SetAsLastSibling();

    }

    public void setTabUnactive()
    {
        //print(this.name + " is unactive");
        this.gameObject.SetActive(true);

    }
}
