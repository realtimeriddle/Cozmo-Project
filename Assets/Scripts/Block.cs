using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Source: GoodDay https://www.assetstore.unity3d.com/en/#!/content/66449

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    static public Block draggedBlock;
    static public GameObject icon;                                                  // Icon of dragged item
    static public Panel sourcePanel;

    public delegate void DragEvent(Block item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event
    static public event DragEvent OnItemDragEndEvent;                               // Drag end event

    private float iconDrop = 0;



    // Use this for initialization
    void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("Start Begin Drag");

        sourcePanel = GetComponentInParent<Panel>();                       // Remember source cell
        draggedBlock = this;                                                         // Set as dragged item
        icon = new GameObject("Icon");                                              // Create object for item's icon
        Image image = icon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = false;                                                // Disable icon's raycast for correct drop handling
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions
        iconRect.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,
                                            GetComponent<RectTransform>().sizeDelta.y);

        //print("Icon created");

        Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas
        if (canvas != null)
        {
            // Display on top of all GUI (in parent canvas)
            icon.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
            icon.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
        }
         if (OnItemDragStartEvent != null)
         {
             OnItemDragStartEvent(this);                                             // Notify all about item drag start
         }

        //print("End Begin Drag");

    }//OnBeginDrag
    public void OnDrag(PointerEventData data)
    {
        if (icon != null)
        {
            icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor
        }

    }//OnDrag
    public void OnEndDrag(PointerEventData eventData)
    {

        if (icon != null)
        {
            setIconDrop(Input.mousePosition.y);
            Destroy(icon);                                                          // Destroy icon on item drop
        }
        if (OnItemDragEndEvent != null)
        {
            OnItemDragEndEvent(this);                                               // Notify all cells about item drag end
        }
        MakeVisible(true);
        draggedBlock = null;
        icon = null;
        sourcePanel = null;

    }//OnEndDrag

    public void killBlock()
    {
        
        Destroy(icon);
        draggedBlock = null;
        icon = null;
        sourcePanel = null;

    } 

    public void setItemIndex(int index)
    {

        this.transform.SetSiblingIndex(index);

    }

    public void setIconDrop(float id)
    {
        iconDrop = id;
    }

    public float getIconDrop()
    {
        float idp = iconDrop;



        return idp;
    }

    public void MakeVisible(bool condition)
    {
        GetComponent<Image>().enabled = condition;
    }//MakeVisible

    public void MakeRaycast(bool condition)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = condition;
        }
    }
}
