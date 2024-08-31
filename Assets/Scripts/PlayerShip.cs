using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShip : Ship
{
    public static PlayerShip Instance { get; private set; }

    public event EventHandler OnLoseGame;
    public event EventHandler OnCollectStar;
    public event EventHandler<OnPlayerHealthChangeEventArgs> OnPlayerHealthChange;
    public class OnPlayerHealthChangeEventArgs : EventArgs
    {
        public float CurrentHealth;
    }
    
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _shield;
    
    private float _shootTimer;

    protected override void Awake()
    {
        base.Awake();
        
        Instance = this;
        
        _curHealth += Upgrades.IncreaseHeroHealthUpgrade;
    }


    protected override void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        base.Update();
        
        Move();
        SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (_shootTimer <= 0) 
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Shoot();
                _shootTimer = _reloadTime - Upgrades.DecreaseReloadTimeUpgrade;
            }
        }
        else 
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        if (other.GetComponent<Star>())
        {
            if (GameManager.Instance.IsBlueTheme)
            {
                Stats.BlueStars++;
            }
            else
            {
                Stats.RedStars++;
            }
            
            OnCollectStar?.Invoke(this, EventArgs.Empty);
            
            Destroy(other.gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        if (damage < 0)
            return;
        
        _curHealth -= damage;
        
        StartChangeColor();
        
        SoundManager.Instance.PlayHitSound();

        if (_curHealth <= 0)
        {
            _curHealth = 0;
            
            Stats.ResetAll();
            OnLoseGame?.Invoke(this, EventArgs.Empty);
        }
        
        OnPlayerHealthChange?.Invoke(this, new OnPlayerHealthChangeEventArgs
        {
            CurrentHealth = _curHealth
        });
    }

    public void ActivateShield()
    {
        _shield.SetActive(true);
    }

    public void ActivateHailOfBullets(int duration)
    {
        UseHailOfBulletsSkill(duration);
    }

    public float GetMaxHealth()
    {
        return _maxHealth + Upgrades.IncreaseHeroHealthUpgrade;
    }
    
    private void Shoot()
    {
        int shots = Upgrades.ShotsAmount;

        switch (shots)
        {
            case 0:
                SingleShoot();
                break;
            case 1:
                DoubleShoot();
                break;
            case 2:
                TripleShoot();
                break;
            case 3:
                QuadShoot();
                break;
            case 4:
                FiveShoot();
                break;
            default:
                FiveShoot();
                break;
        }
    }

    private void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction.normalized * (_movementSpeed * Upgrades.IncreaseMovementSpeedUpgrade);
    }
    
    private void SingleShoot()
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
    }
    
    private void DoubleShoot()
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 2));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 2));
    }
    
    private void TripleShoot()
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
        
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 20));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 20));
    }
    
    private void QuadShoot()
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 2));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 2));
        
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 4));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 4));
    }
    
    private void FiveShoot()
    {
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
        
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 10));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 10));
        
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 20));
    
        ObjectPooler.Instance.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation *
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 20));
    }
    
    private void UseHailOfBulletsSkill(int duration)
    {
        StartCoroutine(HailOfBulletsSkill(duration));
    }

    private IEnumerator HailOfBulletsSkill(int repeats)
    {
        SpawnDeathBullets();

        yield return new WaitForSeconds(0.2f);

        repeats--;

        if (repeats > 0)
        {
            StartCoroutine(HailOfBulletsSkill(repeats));
        }
    }
}