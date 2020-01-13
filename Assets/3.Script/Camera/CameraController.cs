using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraLookSensitivity;
    public float cameraUpRotationLimit;
    public float cameraDownRotationLimit;
    private float currentCameraRotationX, saveCamRotX;

    private float oriCamUpRotLimit, oriCamDownRotLimit;
    public Transform test, test2;
    private Vector3 testPos, test2Pos;
    public GameObject player, theCamera;

    void Start()
    {
        testPos = test.localPosition;
        test2Pos = test2.localPosition;

        oriCamUpRotLimit = cameraUpRotationLimit;
        oriCamDownRotLimit = cameraDownRotationLimit;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * cameraLookSensitivity;
        currentCameraRotationX += cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraDownRotationLimit, cameraUpRotationLimit);

        float fly = 1f;
        if (transform.position.y > 3f) fly = 3f;
        test.localPosition = testPos + Vector3.up * Mathf.Clamp(currentCameraRotationX + 5, -5f, 5f);
        test2.localPosition = test2Pos - (Vector3.up * fly * Mathf.Clamp(currentCameraRotationX, -5f, 15f)) - (Vector3.forward * fly * Mathf.Clamp(currentCameraRotationX, -10f, 10f));
    }

    public void Close(bool _enable, float _upRotLimit, float _downRotLimit)
    {
        if (_enable)
        {
            saveCamRotX = currentCameraRotationX;
            cameraUpRotationLimit = _upRotLimit;
            cameraDownRotationLimit = _downRotLimit;
        }
        else
        {
            currentCameraRotationX = saveCamRotX;
            cameraUpRotationLimit = oriCamUpRotLimit;
            cameraDownRotationLimit = oriCamDownRotLimit;
        }
    }
}
