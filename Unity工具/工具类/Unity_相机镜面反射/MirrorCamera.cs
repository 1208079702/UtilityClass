using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorCamera : MonoBehaviour
{

    private Camera _uiCamera;
    void Start()
    {
        _uiCamera = transform.Find("UICamera").GetComponent<Camera>();

        MirrorCameraDisplay(_uiCamera);
    }

    public static void MirrorCameraDisplay(Camera camera)
    {
        Matrix4x4 matri = camera.projectionMatrix;
        matri *= Matrix4x4.Scale(new Vector3(-1, 1, 1)); // 镜面反射 ，即X轴反向
        camera.projectionMatrix = matri;
    } 
}
