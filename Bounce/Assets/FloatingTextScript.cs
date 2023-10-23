using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextScript : MonoBehaviour
{
  public float destroyFloatingTextTime;
  void Start()
  {
    Destroy(gameObject, destroyFloatingTextTime);  
  }

}
