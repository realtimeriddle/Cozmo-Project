using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Source: GoodDay https://www.assetstore.unity3d.com/en/#!/content/66449

public class Panel : MonoBehaviour, IDropHandler
{

    public enum PanelType
    {
        Side,
        Work,
        View
    }

    public PanelType panelType;

    public struct DropDescriptor                                            // Struct with info about item's drop event
    {
        public Panel sourcePanel;                                  // From this cell item was dragged
        public Panel destinationPanel;                             // Into this cell item was dropped
        public Block item;                                        // dropped item
    }

    void OnEnable()
    {
        Block.OnItemDragStartEvent += OnBlockDragStart;         // Handle any item drag start
        Block.OnItemDragEndEvent += OnBlockDragEnd;             // Handle any item drag end
    }

    void OnDisable()
    {
        Block.OnItemDragStartEvent -= OnBlockDragStart;
        Block.OnItemDragEndEvent -= OnBlockDragEnd;
    }


    // Use this for initialization
    void Start () {

    }

    private void OnBlockDragStart(Block item)
    {
        Block myItem = GetComponentInChildren<Block>(); // Get item from current cell

        if (myItem != null)
        {
            myItem.MakeRaycast(false);                                      // Disable item's raycast for correct drop handling
            if (myItem == item)                                             // If item dragged from this cell
            {
                // Check cell's type
                switch (panelType)
                {
                    case PanelType.Side:
                      
                        break;
                    case PanelType.Work:
                        // Nothing to do
                        break;
                    default:
                        item.MakeVisible(false);                            // Hide item in cell till dragging
                        break;
                }
            }



        }
    }

    private void OnBlockDragEnd(Block item)
    {
        Block myItem = GetComponentInChildren<Block>(); // Get item from current cell
        if (myItem != null)
        {
            if (myItem == item)
            {
                
            }
            myItem.MakeRaycast(true);                                       // Enable item's raycast
        }
        else
        {
            
        }
    }

    public void OnDrop(PointerEventData data)
    {

        if(Block.icon != null)
        {
            if (Block.icon.activeSelf == true)
            {
                Block item = Block.draggedBlock;
                Panel sourcePanel = Block.sourcePanel;
                DropDescriptor desc = new DropDescriptor();

                if ((item != null) && (sourcePanel == this) && (panelType == PanelType.Work))
                {


                    PlaceItem(item.gameObject);                     // Place dropped item in this cell
                                                                // Fill event descriptor
                    desc.item = item;
                    desc.sourcePanel = sourcePanel;
                    desc.destinationPanel = this;
                     // Send message with DragAndDrop info to parents GameObjects
                    StartCoroutine(NotifyOnDragEnd(desc));
                    //StartCoroutine(PositionBlock(desc));





                }

                if ((item != null) && (sourcePanel != this))
                {
                    string itemName = item.name;
                    switch (sourcePanel.panelType)                            // Check source cell's type
                    {
                        
                        case PanelType.Side:
                            
                            item = Instantiate(item);                       // Clone item from source cell
                            item.name = itemName;
                            break;

                        case PanelType.Work:
                            break;
                        default:
                            // Nothing to do
                            break;
                    }

                    switch (panelType)
                    {
                        case PanelType.Side:

                            //Transform deadIcon = this.transform.parent.transform.Find("Icon");

                            //Destroy(deadIcon.gameObject);



                            item.killBlock();

                            Destroy(item.gameObject);


                            break;

                        case PanelType.Work:
                            PlaceItem(item.gameObject);                     // Place dropped item in this cell
                            // Fill event descriptor
                            desc.item = item;
                            desc.sourcePanel = sourcePanel;
                            desc.destinationPanel = this;
                            // Send message with DragAndDrop info to parents GameObjects
                            StartCoroutine(NotifyOnDragEnd(desc));
                            //StartCoroutine(PositionBlock(desc));

                            /*int index = 1;
                            int c = 0;
                            foreach (Transform child in transform)
                            {

                                if (child.name != "PlaceHolder")
                                {
                                    if (child.gameObject != desc.item.gameObject)
                                    {

                                        print("child" + c + " y :" + child.transform.position.y);
                                        print("Item y :" + Block.draggedBlock.getIconDrop());

                                        c++;



                                        if (Block.draggedBlock.getIconDrop() < child.transform.position.y)
                                        {

                                            index++;

                                            print(index);

                                        }

                                    }



                                }

                            }
                            print("Final: " + index);
                            desc.item.setItemIndex(index);*/



                            break;

                        case PanelType.View:
                            PlaceItem(item.gameObject);                     // Place dropped item in this cell
                            // Fill event descriptor
                            desc.item = item;
                            desc.sourcePanel = sourcePanel;
                            desc.destinationPanel = this.transform.GetChild(0).GetComponent<Panel>();



                            // Send message with DragAndDrop info to parents GameObjects
                            StartCoroutine(NotifyOnDragEnd(desc));
                            //print("that darn viewPort!");
                            break;
                        default:
                            // Nothing to do
                            break;

                    }

                }


                if (item.GetComponentInParent<Panel>() == null)   // If item have no cell after drop
                {
                    Destroy(item.gameObject);                               // Destroy it
                }

               



            }

        }


    }

    public void RemoveItem()
    {
        foreach (Block item in GetComponentsInChildren<Block>())
        {
            Destroy(item.gameObject);
        }
    }

    public void PlaceItem(GameObject itemObj)
    {
        //RemoveItem();                                                       // Remove current item from this cell
        if (itemObj != null)
        {
            itemObj.transform.SetParent(transform, false);
            itemObj.transform.localPosition = Vector3.zero;
            PositionBlock(itemObj, Input.mousePosition.y);
            Block item = itemObj.GetComponent<Block>();
            if (item != null)
            {
                item.MakeRaycast(true);
            }
        }

        foreach (Transform child in transform)
        {
            //print("Foreach loop: " + child);

        }//foreach
        //print("Done");
    }

    public Block GetItem()
    {
        return GetComponentInChildren<Block>();
    }

    private IEnumerator NotifyOnDragEnd(DropDescriptor desc)
    {
        // Wait end of drag operation
        while (Block.draggedBlock != null)
        {
            yield return new WaitForEndOfFrame();
        }
        // Send message with DragAndDrop info to parents GameObjects
        gameObject.SendMessageUpwards("OnItemPlace", desc, SendMessageOptions.DontRequireReceiver);
    }

    private void PositionBlock(GameObject item, float mousePosY)
    {

        int index = 1;
        int c = 0;
        foreach (Transform child in transform)
        {

            if (child.name != "PlaceHolder")
            {
                if (child.gameObject != item)
                {

                    print("child" + c + " y :" + child.transform.position.y);
                    print("Item y :" + mousePosY);

                    c++;



                    if (mousePosY < child.transform.position.y)
                    {

                        index++;

                        print(index);

                    }

                }



            }

        }
        print("Final: " + index);
        item.transform.SetSiblingIndex(index);

    }

    /*private IEnumerator PositionBlock(DropDescriptor desc)
    {


        while (Block.draggedBlock != null)
        {
            yield return new WaitForEndOfFrame();
        }


        int index = 1;
        int c = 0;
        foreach (Transform child in transform)
        {

            if (child.name != "PlaceHolder")
            {
                if (child.gameObject != desc.item.gameObject)
                {

                    print("child" + c + " y :" + child.transform.position.y);
                    print("Item y :" + desc.item.getIconDrop());

                    c++;



                    if (desc.item.getIconDrop() < child.transform.position.y)
                    {

                        index++;

                       print(index);

                    }

                }

                

            }

        }
        print("Final: " + index);
        desc.item.setItemIndex(index);



    }*/

 


    // Update is called once per frame
    void Update () {
		
	    }
}
