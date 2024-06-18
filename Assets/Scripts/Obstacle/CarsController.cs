using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarsController : MonoBehaviour
{
    [Header("Object Pool Info")]
    [SerializeField] int poolSize;
    [SerializeField] GameObject hatchback;
    [SerializeField] GameObject police;
    [SerializeField] GameObject sedan;
    [SerializeField] GameObject stationwagon;
    [SerializeField] GameObject taxi;

    [Header("Spawn Position")]
    [SerializeField] Transform left1;
    [SerializeField] Transform right2;
    [SerializeField] Transform left3;
    [SerializeField] Transform right4;

    private List<GameObject> activeCar = new List<GameObject>();    // 오브젝트 정보
    private Queue<GameObject> carPool = new Queue<GameObject>();    // 오브젝트 담기
    private Transform carPoolParent;    // 오브젝트 풀 담을 곳


    void Start()
    {
        carPoolParent = new GameObject("Cars").transform;
        InitializePool();
        StartCoroutine(SpawnCars());
        StartCoroutine(RemoveCars());
    }

    private void InitializePool()
    {
        List<GameObject> carPrefabs = new List<GameObject> { hatchback, police, sedan, stationwagon, taxi };

        for (int i = 0; i < poolSize; i++)
        {
            GameObject carDatas = carPrefabs[Random.Range(0, carPrefabs.Count)];
            GameObject car = Instantiate(carDatas, carPoolParent);
            car.SetActive(false);
            carPool.Enqueue(car);
        }
    }

    IEnumerator SpawnCars()
    {
        while(true)
        {
            if (carPool.Count > 0)
            {
                Transform spawnPos = GetRandomPos();    // 4개 스폰 포인트 중 랜덤 생성
                GameObject car = carPool.Dequeue();
                car.transform.position = spawnPos.position;
                car.transform.rotation = spawnPos.rotation;
                car.SetActive(true);
                activeCar.Add(car);

                CarMovement carMovement = car.GetComponent<CarMovement>();
                if (spawnPos == left1)
                    carMovement.speed = 7;
                else if (spawnPos == right2)
                    carMovement.speed = 4;
                else if (spawnPos == left3)
                    carMovement.speed = 6;
                else
                    carMovement.speed = 5;

                float spawnTime = Random.Range(0, 2.0f);    // 랜덤 스폰 주기
                yield return new WaitForSeconds(spawnTime);
            }
            else
                yield return null;
        }
    }

    private Transform GetRandomPos()
    {
        Transform[] positions = { left1, right2, left3, right4 };
        return positions[Random.Range(0, positions.Length)];
    }

    IEnumerator RemoveCars()
    {
        yield return null;
    }
}
