using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform John;
    public GameObject BulletPrefab;

    private const float FireRate = 0.25f;
    private const float AttackRange = 1.0f;
    private const int MaxHealth = 3;

    private int Health = MaxHealth;
    private float LastShoot;

    private void Update()
    {
        if (John == null) return;

        // Se orienta hacia John y dispara cuando está lo bastante cerca
        float direction = John.position.x - transform.position.x;
        float facing = direction >= 0.0f ? 1.0f : -1.0f;
        transform.localScale = new Vector3(facing, 1.0f, 1.0f);

        bool inRange = Mathf.Abs(direction) < AttackRange;
        if (inRange && Time.time > LastShoot + FireRate)
        {
            Shoot(facing);
            LastShoot = Time.time;
        }
    }

    private void Shoot(float facing)
    {
        Vector3 direction = new Vector3(facing, 0.0f, 0.0f);

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0) Destroy(gameObject);
    }
}
