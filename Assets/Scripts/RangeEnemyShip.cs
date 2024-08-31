using UnityEngine;

public class RangeEnemyShip : EnemyShip
{
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _distToPlayer;
    
    private float _shootTimer;
    private Transform _target;

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
        
        if (Vector2.Distance(transform.position, _target.position) > _distToPlayer) 
        {
            Move();
        }

        SetAngle(_target.position);

        if (_shootTimer <= 0) 
        {
            Shoot();
            _shootTimer = _reloadTime;
        }
        else 
        {
            _shootTimer -= Time.deltaTime;
        }
    }
    
    private void Shoot() 
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
    }
}