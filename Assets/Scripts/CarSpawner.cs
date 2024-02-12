using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> prefabsListLeft = new List<GameObject>();
    public List<GameObject> prefabsListMiddle = new List<GameObject>();
    public List<GameObject> prefabsListRight = new List<GameObject>();
    
    public GameObject spawnPointLeft;
    public GameObject spawnPointMiddle;
    public GameObject spawnPointRight;
    
    public Transform targetPointLeft;
    public Transform targetPointMiddle;
    public Transform targetPointRight;

    public float spawnRadius = 2f;
    
    void Start()
    {
        StartCoroutine(SpawnAtRandom(prefabsListLeft, spawnPointLeft, targetPointLeft));
        StartCoroutine(SpawnAtRandom(prefabsListMiddle, spawnPointMiddle, targetPointMiddle));
        StartCoroutine(SpawnAtRandom(prefabsListRight, spawnPointRight, targetPointRight));
    }
    
    IEnumerator SpawnAtRandom(List<GameObject> prefabsList, GameObject spawnPoint, Transform targetPoint)
    {
        while (true)
        {
            //Car Spawn Rate
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            
            if (prefabsList.Count > 0 && CanSpawn(spawnPoint.transform.position))
            {
                int index = Random.Range(0, prefabsList.Count);
                GameObject prefabToSpawn = Instantiate(prefabsList[index], spawnPoint.transform.position, Quaternion.identity);
                
                CarMovement carMovement = prefabToSpawn.GetComponent<CarMovement>();
                if (carMovement != null)
                {
                    carMovement.target = targetPoint;
                }
            }
        }
    }
    
    bool CanSpawn(Vector2 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, spawnRadius);
        if (hitColliders.Length > 0)
        {
            return false;
        }
        return true;
    }
}
