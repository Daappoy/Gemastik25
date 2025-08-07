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

    public float MoveSpeed = 1f;

    void Start()
    {
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
        // Add Electric input logic here if needed
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
        // Add Electric input logic here if needed
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
