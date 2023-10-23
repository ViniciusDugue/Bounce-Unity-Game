using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// lets there be a custom cursor image over the normal cursor
public class Mouse_Cursor : MonoBehaviour
{
    Camera Camera;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        GameObject playerCameraObject = GameObject.FindGameObjectWithTag("PlayerCamera");
        Camera = playerCameraObject.GetComponent<Camera>();
        Vector2 cursorPos = Camera.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = cursorPos + new Vector2(0.4f,-0.4f);// wow cursor
        transform.position = cursorPos;

    }
}
