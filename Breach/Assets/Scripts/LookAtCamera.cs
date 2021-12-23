using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    LevelManager levelManager;
    Vector3 mainCameraDirection;
    Vector3 finishCameraDirection;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        mainCameraDirection = new Vector3(0, 180, 0);
        finishCameraDirection = new Vector3(0,90,0);
        //transform.Rotate( 180,0,0 );
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (levelManager?.IsMainCameraActive() ?? true)
        {
            var lookPosition = levelManager.CameraPosition() - transform.position;
            lookPosition.y = 0;
            lookPosition.x = 0;

            transform.rotation = Quaternion.LookRotation(lookPosition);
            //transform.rotation = Quaternion.identity;
        }
        else
        {
            var lookPosition = levelManager.CameraPosition() - transform.position;
            lookPosition.y = 0;
            lookPosition.z = 0;

            transform.rotation = Quaternion.LookRotation(lookPosition);

        }
    }
}
