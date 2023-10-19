using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform player; 
    private GameObject playerCamera;
    public AnimationCurve shakeCurve;
    public float cameraLeadOffset;
    void Awake()
    {
        playerCamera = GameObject.FindWithTag("PlayerCamera");
        // DeactivatePlayerCamera();
    }

    void OnEnable()
    {
        ActivatePlayerCamera();
    }

    public void ActivatePlayerCamera()
    {
        playerCamera.SetActive(true);
    }

    public void DeactivatePlayerCamera()
    {
        playerCamera.SetActive(false);
    }
    
    void Update()
    {
        LeadCameraPosition();
        // if (GameObject.FindGameObjectWithTag("Player") !=null)
        // {
        //     playerCamera = GameObject.FindWithTag("PlayerCamera");
        //     player = GameObject.FindGameObjectWithTag("Player").transform;
        //     //makes sure that during replays, the camera position doesnt go back on the z axis
        //     Vector3 newPosition = player.position;
        //     newPosition.z = -5;
        //     transform.position = newPosition;
        //     playerCamera.GetComponent<Camera>().orthographicSize = 8f;
        // }
        // else
        // {
        //     playerCamera.GetComponent<Camera>().orthographicSize = 5f;
        // }
       
    }
    // leads camera position in between playerposition and cursor position
    public void LeadCameraPosition()
    {
        
        if (GameObject.FindGameObjectWithTag("Player") !=null)
        {
            if(Time.timeScale==1f)
            {
                playerCamera = GameObject.FindWithTag("PlayerCamera");
                Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector3 mousePosition = playerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).z);
                Vector3 mouseDirection = (mousePosition - playerPosition).normalized;

                //makes sure that during replays, the camera position doesnt go back on the z axis
                transform.position = new Vector3(playerPosition.x, playerPosition.y, -5) + mouseDirection * cameraLeadOffset;
                playerCamera.GetComponent<Camera>().orthographicSize = 8f;
            }
            
        }
        else
        {
            playerCamera.GetComponent<Camera>().orthographicSize = 5f;
        }
    }

    public void ShakeCamera(float shakeDuration)
    {
        print("Start camera shake");
        StartCoroutine(Shaking(shakeDuration));
    }

    // public IEnumerator Shaking(float shakeDuration)
    // {   
        
    //     Vector3 startPosition = transform.position;
    //     float elapsedTime = 0f;
    //     while(elapsedTime <shakeDuration)
    //     {
    //         startPosition = transform.position;
    //         elapsedTime += Time.deltaTime;
    //         float shakeStrength =shakeCurve.Evaluate(elapsedTime / shakeDuration)/ 7.0f;
    //         transform.position =  startPosition + Random.insideUnitSphere * shakeStrength;
    //         print(transform.position);
    //         yield return null;
    //     }
    //     transform.position =  startPosition;
    // }
    public IEnumerator Shaking(float shakeDuration)
    {   
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        int shakeCounter = 0;
        while(elapsedTime < shakeDuration)
        {
            startPosition = transform.position;
            elapsedTime += Time.deltaTime;
            if (shakeCounter < 1) // Only shake every third frame
            {
                float shakeStrength = shakeCurve.Evaluate(elapsedTime / shakeDuration) / 11.0f; // Adjust the divisor to reduce frequency
                transform.position = startPosition + Random.insideUnitSphere * shakeStrength;
                print(transform.position);
            }
            shakeCounter = (shakeCounter + 1) % 2; // Increment counter and wrap around to 0 after reaching 2
            yield return null;
        }
        transform.position = startPosition;
    }

}
