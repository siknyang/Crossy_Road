using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Car")]
public class CarData : ScriptableObject
{
    [Header("Info")]
    public string carName;
    public GameObject carPrefab;
}
