using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public explorerData data;
    CameraControl cameraControl;

    private void Start()
    {
        data = new explorerData();
        data.explorerObject = this.gameObject;
        data.locationPoints.Add(this.gameObject.transform.position);
        data.timePoints.Add(Time.realtimeSinceStartup);
        
        if(GetComponentInChildren<CameraControl>())
        { 
            cameraControl = GetComponentInChildren<CameraControl>();
        }else
        {
            Debug.LogError("No CameraControl script on child object");
        }
    }
}
