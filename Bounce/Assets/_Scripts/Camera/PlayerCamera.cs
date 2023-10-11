using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform player; 
    private GameObject playerCameraController;
    
    void Awake()
    {
        playerCameraController = GameObject.FindWithTag("PlayerCamera");
        // DeactivatePlayerCamera();
    }

    void OnEnable()
    {
        ActivatePlayerCamera();
    }

    public void ActivatePlayerCamera()
    {
        playerCameraController.SetActive(true);
    }

    public void DeactivatePlayerCamera()
    {
        playerCameraController.SetActive(false);
    }
    
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("Player") !=null)
        {
            playerCameraController = GameObject.FindWithTag("PlayerCamera");
            player = GameObject.FindGameObjectWithTag("Player").transform;
            //makes sure that during replays, the camera position doesnt go back on the z axis
            Vector3 newPosition = player.position;
            newPosition.z = -5;
            transform.position = newPosition;
            playerCameraController.GetComponent<Camera>().orthographicSize = 8f;
        }
        else
        {
            playerCameraController.GetComponent<Camera>().orthographicSize = 5f;
        }
       
    }

}
