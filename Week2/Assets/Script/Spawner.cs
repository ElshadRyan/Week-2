using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] FruitPrefab;

    public GameObject BombPrefab;

    [Range(0f, 1f)]
    public float BombChance = 0.05f; 

    public float MinSpawnDelay = 0.25f;
    public float MaxSpawnDelay = 1f;

    public float MinAngle = -15f;
    public float MaxAngle = 15f;

    public float MinForce = 18f;
    public float MaxForce = 22f;

    public float MaxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();   
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));

        while (enabled)
        {
            GameObject Prefab = FruitPrefab[Random.Range(0, FruitPrefab.Length)];

            if (Random.value < BombChance)
            {
                Prefab = BombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion Rotation = Quaternion.Euler(0f, 0f, Random.Range(MinAngle, MaxAngle));

            GameObject Fruit =  Instantiate(Prefab, position, Rotation);

            Destroy(Fruit, MaxLifetime);

            float Force = Random.Range(MinForce, MaxForce);
            Fruit.GetComponent<Rigidbody>().AddForce(Fruit.transform.up * Force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
        }
    }
}
