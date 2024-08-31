using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    
    private float _curHealth;

    private void Awake()
    {
        _curHealth = _maxHealth + Upgrades.IncreaseShieldHealthUpgrade;
    }

    public void TakeDamage(float damage) 
    {
        if (damage < 0)
            return;
        
        _curHealth -= damage;

        if (_curHealth <= 0) 
        {
            gameObject.SetActive(false);
            _curHealth = _maxHealth + Upgrades.IncreaseShieldHealthUpgrade;
        }
    }
}