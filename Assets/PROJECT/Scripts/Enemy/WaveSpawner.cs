using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    int waveCount = -1;
    public List<Wave> waves = new();
    public Transform spawnPosition;
    public void StartNextWave()
    {
        waveCount++;
        StartCoroutine(WaveCoroutine());
    }
    private void Start()
    {
        waveCount = PlayerPrefs.GetInt("Wave");
    }
    IEnumerator WaveCoroutine()
    {
        GameManager.Instance.isWaveGoing = true;
        var wave = waves[waveCount];
        PlayerPrefs.SetInt("Wave", waveCount);
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Vector2 circle = Random.insideUnitCircle.normalized * 15;
            var enemy = Instantiate(wave.enemies[i].prefab, new Vector3(circle.x, 0, circle.y), Quaternion.identity);
            yield return new WaitForSeconds(wave.delays[i]);
        }
        while(FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length > 0)
        {           
            yield return new WaitForSeconds(.5f);
        }
        GameManager.Instance.isWaveGoing = false;
        yield return null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartNextWave();
        }
    }
}