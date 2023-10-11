using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// magika bar script for player
public class Magika_Bar_Script : MonoBehaviour
{
    
    Slider _magikaSlider;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        _magikaSlider = GetComponent<Slider>();
    }
    void Update()
    {
        SetMagika(player.GetComponent<Unit>().magika);
    }
    public void SetMaxMagika(float maxMagika)
    {
        _magikaSlider.maxValue = maxMagika;
        _magikaSlider.value  =maxMagika;
    }
    
     public void SetMagika(float magika)
    {
        _magikaSlider.value  = magika;
    }
}
