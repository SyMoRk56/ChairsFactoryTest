using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int currentHp;
    public BuildingScriptableObject building;
    public int upgrade = 0;
    int maxUpgrade;
    MeshRenderer[] renderers;
    List<Material> materials = new();
    public BoxCollider collider;

    List<GameObject> touchingEnemies = new();
    float damageTimer = 0f;
    public float damageInterval = 1f;

    public void Start()
    {
        collider = GetComponentInChildren<BoxCollider>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var ren in renderers)
        {
            materials.Add(ren.material);
        }
        collider.enabled = false;
    }

    public virtual void Setup()
    {
        collider.enabled = true;
        maxUpgrade = Mathf.Min(building.levelsHp.Count, building.levelsCost.Count);
        upgrade = Mathf.Clamp(upgrade, 0, maxUpgrade - 1);
        currentHp = building.levelsHp[upgrade];
        SetDefaultMat();
    }

    public void SetRedMat()
    {
        foreach (var renderer in renderers)
        {
            renderer.material = GameManager.Instance.redMat;
        }
    }

    public void SetGreenMat()
    {
        foreach (var renderer in renderers)
        {
            renderer.material = GameManager.Instance.greenMat;
        }
    }

    public void SetDefaultMat()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = materials[i];
        }
    }

    public virtual void Upgrade()
    {
        if (upgrade + 1 >= maxUpgrade) return;
        if (!CheckMoney()) return;
        GameManager.Instance.money -= building.levelsCost[upgrade + 1];
        upgrade++;
        print("Upgrade");
        currentHp = building.levelsHp[upgrade];
        OnUpgrade();
    }

    public virtual void TakeDamage(int damage)
    {
        print("Take damage " + damage);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool CheckMoney() => GameManager.Instance.money >= (!collider.enabled ? building.levelsCost[0] : building.levelsCost[upgrade + 1]);

    public virtual void OnUpgrade()
    {
    }

    private void Update()
    {
        if (touchingEnemies.Count > 0)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                List<Enemy> enemies = new List<Enemy>();
                foreach(var e in touchingEnemies)
                {
                    if(e == null) touchingEnemies.Remove(e);
                    enemies.Add(e.GetComponent<Enemy>());
                }
                TakeDamage(enemies.Max(p => p.enemy.damage));
                damageTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !touchingEnemies.Contains(collision.gameObject))
        {
            touchingEnemies.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && touchingEnemies.Contains(collision.gameObject))
        {
            touchingEnemies.Remove(collision.gameObject);
        }
    }
}
