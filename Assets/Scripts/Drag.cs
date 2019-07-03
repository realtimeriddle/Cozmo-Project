using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    private bool dragging = false;
    private float distance;

    // Use this for initialization
    void Start () {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

        UnityEngine.Debug.Log(string.Format("Found {0} overlapping object(s)", overlap.Length - 1));

    }//Start

    void OnMouseDown()
    {
        UnityEngine.Debug.Log("Clicked");

        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }//OnMouseDown

    void OnMouseUp()
    {
        dragging = false;
        UnityEngine.Debug.Log("ended");

    }//OnMouseUp

    // Update is called once per frame
    void Update () {

        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }//if



    }//Update
}//DragRed
