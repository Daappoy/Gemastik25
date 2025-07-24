using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHold : MonoBehaviour
{
    public int DoorHID;
    public bool isOpen = false;
    public ButtonHold ButtonHold;
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

    void Update()
    {
        TargetPos = InitialPos + offset;
        if (ButtonHold.isPressed == true)
        {
            TargetPos = InitialPos +offset;
            OpenDoorHold();
        }
        else
        {
            TargetPos = InitialPos;
            CloseDoorHold();
        }
    }

    public void OpenDoorHold()
    {
        if (ButtonHold.isPressed == true)
        {
            isOpen = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPos, MoveSpeed * Time.deltaTime);
        }
    }
    public void CloseDoorHold()
    {
        if (ButtonHold.isPressed == false)
        {
            isOpen = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPos, MoveSpeed * Time.deltaTime);
        }
    }
}
