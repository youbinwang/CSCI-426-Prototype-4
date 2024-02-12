using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box; 
    public List<GameObject> spawnPoints = new List<GameObject>();
    public float checkRadius = 0.5f;

    void Start()
    {
        StartCoroutine(SpawnBox());

    }

    IEnumerator SpawnBox()
    {
        while (true)
        {
            float waitTime = Random.Range(2f, 6f);
            yield return new WaitForSeconds(waitTime);

            Spawn();
        }

        void Spawn()
        {
            
            int randomIndex = Random.Range(0, spawnPoints.Count);
            GameObject selectedSpawnPoint = spawnPoints[randomIndex];

            Vector3 spawnPosition = selectedSpawnPoint.transform.position;


            if (!Physics2D.OverlapCircle(spawnPosition, checkRadius))
            {
                //Spawn Box
                Instantiate(box, spawnPosition, Quaternion.identity);
            }
        }
    }
}
