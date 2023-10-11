using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //if there are no other items in that slot already, drop the item
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if(transform.childCount ==0)
        {
            draggableItem.parentAfterDrag = transform; 
        }
        //else if there is an item already, then swap the two items
        else if(transform.childCount ==1)
        {
            DraggableItem itemInSlot = transform.GetChild(0).gameObject.GetComponent<DraggableItem>();
            itemInSlot.PlaceItem(draggableItem.parentAfterDrag);
            draggableItem.parentAfterDrag = transform;
        }
    }
}

