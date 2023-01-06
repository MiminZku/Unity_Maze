using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject target;
    public GameObject arrowPrefab;
    public GameObject lever;
    public Transform arrowSpawnPos;

    float timeAfterSpawn = 0;
    float spawnRateMin = 0.5f;
    float spawnRateMax = 2f;
    float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if (lever.GetComponent<Switch>().isSwitchOn)
        {
            if(timeAfterSpawn >= spawnRate)
            {
                Vector3 lookPosition = target.transform.position;
                lookPosition.y = gameObject.transform.position.y;
                transform.LookAt(lookPosition);

                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
                timeAfterSpawn = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPos.position, arrowSpawnPos.rotation);
        Rigidbody arrorRb = arrow.GetComponent<Rigidbody>();
        arrorRb.AddForce(arrowSpawnPos.up * 20,ForceMode.Impulse);
    }
}
