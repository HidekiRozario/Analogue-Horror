using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : CinemachineExtension
{
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float clampAngle;
    
    [SerializeField] private Transform canvasCamera;

    private Vector3 startingRotation;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime){
        if (vcam.Follow){
            if(stage == CinemachineCore.Stage.Aim){
                if(startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector3 deltaInput = Vector3.down * Input.GetAxis("Mouse Y") + Vector3.right * Input.GetAxis("Mouse X");
                startingRotation += deltaInput * sensitivity * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                canvasCamera.rotation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
