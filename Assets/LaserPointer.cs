using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserPointer : MonoBehaviour
{
    public enum STATE { OFF, ON, PAUSED };

    public STATE state;

    private Light laserLight;
    private GameObject OnLight;

    public Action currentBehavior;

    public LayerMask layerMask;
    private Vector3 pointerOffset;

    // Start is called before the first frame update
    private void Start()
    {
        OnLight = transform.GetChild(0).gameObject;

        laserLight = GetComponent<Light>();
        pointerOffset = new Vector3(0, transform.position.y, 0);

        ChangeBehaviorTo(STATE.OFF);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == STATE.OFF)
                ChangeBehaviorTo(STATE.ON);
            else
                ChangeBehaviorTo(STATE.OFF);
        }
        currentBehavior();
    }

    public void ChangeBehaviorTo(STATE newState)
    {
        //Do not change state if state is the same
        // if (newState == state) return;

        //Else
        switch (newState)
        {
            case STATE.OFF:
                currentBehavior = Pointer_OFF;
                OnLight.SetActive(false);
                break;

            case STATE.ON:
                currentBehavior = Pointer_ON;
                OnLight.SetActive(true);
                break;

            case STATE.PAUSED:
                currentBehavior = Pointer_Paused;
                break;
        }
        state = newState;
    }

    #region Behaviors

    private void Pointer_OFF()
    {
        PostionPointerWithMouse();
    }

    private void Pointer_ON()
    {
        PostionPointerWithMouse();
    }

    private void Pointer_Paused()
    {
    }

    #endregion Behaviors

    private void PostionPointerWithMouse()
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