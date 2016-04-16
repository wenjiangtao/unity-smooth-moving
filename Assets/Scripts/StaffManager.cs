using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StaffManager : MonoBehaviour {

    private static StaffManager _instance;
    public static StaffManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<StaffManager>();
            if (_instance == null)
            {
                Debug.LogError("StaffManager should be at the scene.");
            }
        }
        return _instance;
    }

    public Vector3 firstStaffPosition = new Vector3(15f, 0f, 0f);
    public Vector3 direction = Vector3.left;
    public Vector3 distanceBetweenStaffs = new Vector3(1f, 0f, 0f);
    public float staffSpeed = 10f;
    public int length = 20;
    public StaffController[] staffPrefabs;
    public InputField staffSpeedInputField;
    public Toggle patternToggle;
    
    private StaffController _firstDisplayStaffController;
    private StaffController _lastDisplayStaffController;

    private void InitStaff(StaffController staffController, StaffController lastStaffController)
    {
        staffController.SetPosition(lastStaffController.GetPosition() + distanceBetweenStaffs);
        lastStaffController.NextDisplayStaffController = staffController;
    }

    public void UpdateStaffSpeed()
    {
        try
        {
            staffSpeed = float.Parse(staffSpeedInputField.text);
        }
        catch
        {
            Debug.LogError("Input Error"); 
        }
    }

    void Awake()
    {
        for (int i = 0; i < length; i++)
        {
            StaffController staffController;
            staffController = Instantiate(staffPrefabs[Random.Range(0, staffPrefabs.Length - 1)]);
            if (i == 0)
            {
                staffController.SetPosition(firstStaffPosition);
                _firstDisplayStaffController = staffController;
            }
            else
            {
                InitStaff(staffController, _lastDisplayStaffController);
            }
            if (_lastDisplayStaffController)
            {
                _lastDisplayStaffController.NextDisplayStaffController = staffController;
            }
            _lastDisplayStaffController = staffController;
        }
        
    }

    void LateUpdate()
    {
        if (_lastDisplayStaffController.GetPosition().x < firstStaffPosition.x)
        {
            InitStaff(_firstDisplayStaffController, _lastDisplayStaffController);
            _lastDisplayStaffController = _firstDisplayStaffController;
            _firstDisplayStaffController = _firstDisplayStaffController.NextDisplayStaffController;
        }
    }
}
