using System.Collections;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    public GameObject[] bullets;
    public int[] xBulletSpawn; // to choose if the bullet will spawn right or left
    //private float xBulletSpawn = -10.0f;
    //private float zSpawnRange = 10.0f; we arr going to use only x and y
    private float ySpawn = 5f;
    private float bulletSpawnTime = 1.0f;
    private float startDelay = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(startDelay);
        while(GameManager.Instance.isGameActive)
        {
            SpawnRandomBullet();
            yield return new WaitForSeconds(bulletSpawnTime);
        }
    }

    public void SpawnRandomBullet()
    {
        float randomY = Random.Range(-ySpawn, ySpawn );
        int xRandomPosition = xBulletSpawn[Random.Range(0, xBulletSpawn.Length)];
        //int randomIndex = Random.Range(0, bullets.Length);

        Vector3 spawnPos = new Vector3(xRandomPosition, randomY, 0);

        GameObject pooledbullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (pooledbullet != null)
        {
            pooledbullet.transform.position = spawnPos;
            pooledbullet.SetActive(true);
        }
    }

}
