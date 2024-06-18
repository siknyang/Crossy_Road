using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
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
        if (carPool.Count > 0 )
        {
            StartCoroutine(RemoveCars());
        }
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

                // float spawnTime = Random.Range(0, 2.0f);    
                yield return new WaitForSeconds(0.5f);     // 랜덤 스폰 주기
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
        while (true)
        {
            for (int i = activeCar.Count -1 ; i >=0; i--)
            {
                if (activeCar[i].transform.position.x < 24.0f || activeCar[i].transform.position.x > 69.0f)   // 자동차 경계 확인
                {
                    activeCar[i].SetActive(false);  // 오브젝트 비활성화
                    carPool.Enqueue(activeCar[i]);  // 오브젝트 풀에 추가
                    activeCar.RemoveAt(i);  // 리스트에서 해당 오브젝트 제거
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
