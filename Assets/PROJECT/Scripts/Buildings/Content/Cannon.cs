using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Building
{
    public List<int> damageUpgrades = new List<int>();

    
    public IEnumerator Start()
    {
        base.Start();
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
                if(minDist < 6)
                closestEnemy.TakeDamage(damageUpgrades[upgrade]);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
