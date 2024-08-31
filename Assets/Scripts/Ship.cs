using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Ship : MonoBehaviour, IDamageable
{
    [SerializeField] protected PoolTag _projectileTag;
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _angleOffset;
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] private SpriteRenderer _shipVisual;
    [SerializeField] protected float _maxHealth;
    [SerializeField] private float _hitColorChangingSpeed;
    
    protected Rigidbody2D _rigidbody;
    protected float _curHealth;
    private bool _isChangingToHitColor;
    private float _greenAndBlueComponent;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _curHealth = _maxHealth;
        _greenAndBlueComponent = 1f;
    }

    protected virtual void Update()
    {
        ChangeColor();
    }

    public abstract void TakeDamage(float damage);

    protected void SetAngle(Vector3 targetPosition) 
    {
        Vector3 deltaPosition = targetPosition - transform.position;
        float angleZ = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0f, 0f, angleZ + _angleOffset);

        transform.rotation = Quaternion.Lerp(transform.rotation, angle, _rotationSpeed * Time.deltaTime);
    }

    protected void StartChangeColor()
    {
        _isChangingToHitColor = true;
        _greenAndBlueComponent = 1f;
    }
    
    protected void SpawnDeathBullets()
    {
        for (int i = 0; i < 18; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(_projectileTag, transform.position, transform.rotation *
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 20 * i));
        }
    }
    
    private void ChangeColor()
    {
        if (_isChangingToHitColor)
        {
            _greenAndBlueComponent -= Time.deltaTime * _hitColorChangingSpeed;

            if (_greenAndBlueComponent <= 0)
            {
                _isChangingToHitColor = false;
            }
        }
        else
        {
            _greenAndBlueComponent += Time.deltaTime * _hitColorChangingSpeed;
        }
	
        _shipVisual.color = new Color(1, _greenAndBlueComponent, _greenAndBlueComponent);
    }
}