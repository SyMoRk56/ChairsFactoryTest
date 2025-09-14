using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Building
{
    public List<int> damageUpgrades = new List<int>();
    public List<Material> materialUpgrades = new List<Material>();
    public Transform turret;
    float shootTimer;
    public GameObject bulletPrefab;
    public MeshRenderer bodyRenderer;

    private void Start()
    {
        StartCoroutine(TowerRoutine());
    }

    private IEnumerator TowerRoutine()
    {
        while (true)
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            if (enemies.Length > 0)
            {
                Enemy closestEnemy = null;
                float minDist = Mathf.Infinity;

                foreach (var enemy in enemies)
                {
                    float dist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestEnemy = enemy;
                    }
                }

                if (closestEnemy != null && minDist < 6f)
                {
                    Vector3 direction = closestEnemy.transform.position - turret.position;
                    direction.y = 0;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    turret.rotation = Quaternion.Slerp(
                        turret.rotation,
                        targetRotation,
                        Time.deltaTime * 15
                    );
                    if(shootTimer > 2)
                    {
                        if(Quaternion.Angle(turret.rotation, targetRotation) < 5)
                        {
                            shootTimer = 0;
                            GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, Quaternion.identity);
                            bullet.GetComponent<Bullet>().Setup(turret.forward, damageUpgrades[upgrade]);
                        }
                        
                    }
                }
            }
            shootTimer += Time.deltaTime;
            yield return null;
        }
    }
    public override void OnUpgrade()
    {
        print("Cannon on upgrade: " + upgrade);
        base.OnUpgrade();
        bodyRenderer.material = materialUpgrades[upgrade];
    }
    public override void Setup()
    {
        base.Setup();
        bodyRenderer.material = materialUpgrades[upgrade];
    }
}
