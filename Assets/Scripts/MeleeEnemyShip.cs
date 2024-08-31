using UnityEngine;

public class MeleeEnemyShip : EnemyShip
{
    [SerializeField] private int _damage;
    [SerializeField] private float _hitCoolDown;
    
    private Transform _target;
    private float _hitTimer;

    private void Start()
    {
        _target = PlayerShip.Instance.transform;
    }

    protected override void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        base.Update();
        
        if (_target == null)
            return;
        
        if (_hitTimer <= 0)
        {
            Move();
            SetAngle(_target.position);
        }
        else 
        {
            _hitTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        if (collision.gameObject.TryGetComponent(out IDamageable target) && _hitTimer <= 0)
        {
            target.TakeDamage(_damage);
            _hitTimer = _hitCoolDown;
        }
    }
}