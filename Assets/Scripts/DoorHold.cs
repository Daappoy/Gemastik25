using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHold : MonoBehaviour
{
    public int DoorHID;
    public enum DoorInputType { Router, ButtonHold, Electric }
    public DoorInputType inputType = DoorInputType.ButtonHold;
    public bool isOpen = false;
    public ButtonHold ButtonHold;
    public Router Router;
    public ElectricBox ElectricBox;
    [SerializeField]
    private Vector3 InitialPos;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 TargetPos;
    public AudioManager audioManager;
    public bool isOnTargetPosition = true;
    private bool wasDoorSoundPlaying = false;

    public float MoveSpeed = 1f;

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        InitialPos = transform.localPosition;
        if (inputType == DoorInputType.ButtonHold)
        {
            ButtonHold[] buttons = FindObjectsOfType<ButtonHold>();
            foreach (ButtonHold button in buttons)
            {
                if (button.ButtonHID == DoorHID)
                {
                    ButtonHold = button;
                    break;
                }
            }
        }
        else if (inputType == DoorInputType.Router)
        {
            Router[] routers = FindObjectsOfType<Router>();
            foreach (Router router in routers)
            {
                if (router.RouterHID == DoorHID)
                {
                    Router = router;
                    break;
                }
            }
        }
       
        else if (inputType == DoorInputType.Electric)
        {
            ElectricBox[] electricBoxes = FindObjectsOfType<ElectricBox>();
            foreach (ElectricBox electricBox in electricBoxes)
            {
                if (electricBox.ElectricBoxHID == DoorHID)
                {
                    ElectricBox = electricBox;
                    break;
                }
            }
        }
    }

    void Update()
    {
        TargetPos = InitialPos + offset;
        
        Vector3 previousPosition = transform.localPosition;
        
        if (inputType == DoorInputType.ButtonHold && ButtonHold != null)
        {
            if (ButtonHold.isPressed)
            {
                OpenDoorHold();
            }
            else
            {
                CloseDoorHold();
            }
        }
        else if (inputType == DoorInputType.Router && Router != null)
        {
            if (Router.isPressed)
            {
                OpenDoorHold();
            }
            else
            {
                CloseDoorHold();
            }
        }
        else if (inputType == DoorInputType.Electric && ElectricBox != null)
        {
            if (ElectricBox.isPressed)
            {
                OpenDoorHold();
            }
            else
            {
                CloseDoorHold();
            }
        }
        
        bool doorIsMoving = Vector3.Distance(previousPosition, transform.localPosition) > 0.001f;
        Vector3 currentTarget = isOpen ? TargetPos : InitialPos;
        isOnTargetPosition = Vector3.Distance(transform.localPosition, currentTarget) < 0.1f;
        if (doorIsMoving && !wasDoorSoundPlaying)
        {
            audioManager.PlaySFX(audioManager.DoorSound);
            wasDoorSoundPlaying = true;
        }
        else if (!doorIsMoving && wasDoorSoundPlaying)
        {
            wasDoorSoundPlaying = false;
        }
    }

    public void OpenDoorHold()
    {
        isOpen = true;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPos, MoveSpeed * Time.deltaTime);
    }
    public void CloseDoorHold()
    {
        isOpen = false;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, InitialPos, MoveSpeed * Time.deltaTime);
    }
}
