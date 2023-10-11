using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera playerCamera;
    public Image image;
    private Vector3 offset;
    [HideInInspector] public Transform parentAfterDrag;
    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>()!=null)
        {
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        }
    }
    public void OnEnable()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        offset = transform.position - playerCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.SetParent(transform.parent.parent.parent);
        image.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = playerCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
    }
     
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void PlaceItem(Transform parentAfterPlacement)
    {
        parentAfterDrag = parentAfterPlacement;
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
