using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Bounce meter script for player
public class BounceMeterScript : MonoBehaviour
{
    
    Slider bounceSlider;
    [SerializeField] private BallController ballScript;
    public PlayerController playerScript;
    public PlayerCamera playerCameraScript;
    private void Start()
    {
        bounceSlider = GetComponent<Slider>();
    }
    void Update()
    {
        SetMaxBounceMeter(playerScript.maxBallSpeed);
        SetBounceMeter(playerScript.storedChargeSpeed);
        float bounceMeterNotchSize = playerScript.maxBallSpeed/5;
        print(bounceSlider.value);
        if (bounceSlider.value % bounceMeterNotchSize ==0)
        {
            playerCameraScript.ShakeCamera(0.3f);
        }
    }
    public void SetMaxBounceMeter(float maxBounce)
    {
        bounceSlider.maxValue = maxBounce;
        bounceSlider.value  = maxBounce;
    }
    
    public void SetBounceMeter(float bounce)
    {
        bounceSlider.value  = bounce;
    }
}
