using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInfo : MonoBehaviour
{

    private DeviceInfo Instance;

    BatteryStatus batteryStatus;
    DeviceType deviceType;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(batteryStatus);
        Debug.Log(deviceType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
