using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    private GameObject player;
    private GameObject shield;
    private Camera playerCamera;
    public float shieldDistance;
    void Start()
    {
        shield = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        
    }

    void Update()
    {
        // make shield game object follow player mouse while rotating around player in a short radius
        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition)-new Vector3(0,0,playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
        Vector3 direction = mousePosition - player.transform.position;
        Vector3 shieldDirection = (mousePosition-player.transform.position).normalized;
        shield.transform.position = player.transform.position + shieldDirection *shieldDistance;
        // print(shield.transform.position);
        shield.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction)* Quaternion.Euler(0, 0, -130);
    }
}
