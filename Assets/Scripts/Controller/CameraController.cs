using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera { get; private set; }
    public Transform characterPos;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 newPos = characterPos.position + offset;
        mainCamera.transform.position = newPos;
    }
}
