using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public enum STATE { OFF, ON, PAUSED };
    public STATE state;

    Light laserLight;

    public Action currentBehavior;

    public LayerMask layerMask;
    Vector3 pointerOffset;

    // Start is called before the first frame update
    void Start()
    {
        state = STATE.OFF;
        
        currentBehavior = Pointer_OFF;
        pointerOffset = new Vector3(0, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentBehavior();
    }

    #region Behaviors
    void Pointer_OFF()
    {

    }
    void Pointer_On()
    {



    }

    void Pointer_Paused()
    {

    }

    #endregion

    void PostionPointerWithMouse()
    {

        //Determine if walkable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create ray obj from camera to click point
        RaycastHit hit;

        //If ray hit walkable area
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) //cast ray. if hit land, move
        {
            transform.position = hit.point + pointerOffset;
        }
    }
}
