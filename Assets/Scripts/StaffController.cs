using UnityEngine;
using System.Collections;
using System;

public class StaffController : MonoBehaviour {

    public Transform rigidbodyStaff;
    public Transform nonRigidbodyStaff;

    private Vector3 _previousPosition;

    void Update()
    {
        if (StaffManager.GetInstance().patternToggle.isOn)
        {
            float lerpFactor = ((Time.time - Time.fixedTime) / Time.fixedDeltaTime);
            nonRigidbodyStaff.position = Vector3.Lerp(_previousPosition, rigidbodyStaff.position, lerpFactor);
        }
        else
        {
            nonRigidbodyStaff.position += Vector3.left * Time.smoothDeltaTime * StaffManager.GetInstance().staffSpeed;
        }
    }

    void FixedUpdate()
    {
        _previousPosition = rigidbodyStaff.position;

        rigidbodyStaff.GetComponent<Rigidbody>().velocity = Vector3.left * StaffManager.GetInstance().staffSpeed;
    }

    public void SetPosition(Vector3 position)
    {
        nonRigidbodyStaff.position = position;
        rigidbodyStaff.position = position;
    }

    public Vector3 GetPosition()
    {
        return rigidbodyStaff.position;
    }

    public StaffController NextDisplayStaffController { get; set; }

}
