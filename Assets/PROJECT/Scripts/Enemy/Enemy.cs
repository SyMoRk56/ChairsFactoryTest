using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemy;
    float hp;
    Transform player;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = enemy.hp;
    }
    public void TakeDamage(int damage)
    {
        print("Enemy Take Damage " + damage);
        hp-= damage;
        if(hp <= 0)
        {
            GameManager.Instance.money += enemy.moneyDrop;
            Destroy(gameObject);
        }
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + (player.position - transform.position).normalized * enemy.moveSpeed * Time.fixedDeltaTime);
    }
}
