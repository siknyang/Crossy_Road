using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    [Header("Object Pool Info")]
    [SerializeField] int poolSize;
    [SerializeField] ScriptableObject hatchback;
    [SerializeField] ScriptableObject police;
    [SerializeField] ScriptableObject sedan;
    [SerializeField] ScriptableObject stationwagon;
    [SerializeField] ScriptableObject taxi;

    [Header("Spawn Position")]
    [SerializeField] Transform left1;
    [SerializeField] Transform right2;
    [SerializeField] Transform left3;
    [SerializeField] Transform right4;

    private List<GameObject> activeCar = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpawnCars();
        }
    }
    
    void Update()
    {
        // 생성 조건


        // 제거 조건
        //if ()
    }

    private void SpawnCars()
    {
        throw new NotImplementedException();
    }

    private void RemoveCars()
    {

    }
}
