using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Bounce meter script for player
public class BounceMeterScript : MonoBehaviour
{
    
    Slider bounceSlider;
    [SerializeField] private BallController ballScript;

    private void Start()
    {
        bounceSlider = GetComponent<Slider>();
    }
    void Update()
    {
        SetMaxBounceMeter(ballScript.maxBounce);
        SetBounceMeter(ballScript.bounce);
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
