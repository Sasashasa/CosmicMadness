using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damage;

    private void Update()
    {
        transform.Translate(Vector2.down * (_movementSpeed * Upgrades.IncreaseBulletSpeedUpgrade * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}