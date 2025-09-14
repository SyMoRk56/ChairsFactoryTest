using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity;
    int damage;
    Vector3 forward;
    public void Setup(Vector3 forward, int damage)
    {
        this.damage = damage;
        this.forward = forward;
    }
    private void Update()
    {
        transform.position += forward * velocity * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(9999);
            Destroy(gameObject);
        }
    }
}
