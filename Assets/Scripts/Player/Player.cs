using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public CameraController controller;

    void Start()
    {
        GameManager.Instance.Player = this;
        // controller = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
