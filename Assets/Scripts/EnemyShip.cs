using UnityEngine;

public class EnemyShip : Ship
{
	[SerializeField] private GameObject _bloodVFX;
	[SerializeField] private GameObject _star;
	
	public override void TakeDamage(float damage) 
	{
		if (!GameManager.Instance.IsGamePlaying)
			return;
		
		if (damage < 0)
			return;
		
		float realDamage = GetRealDamage(damage);
        
		_curHealth -= realDamage;
        
		StartChangeColor();
        
		SoundManager.Instance.PlayHitSound();

		if (_curHealth <= 0)
		{
			if (GameManager.Instance.IsBlueTheme)
			{
				Instantiate(_star, transform.position, Quaternion.identity);
			}
			else
			{
				SpawnDeathBullets();
			}
            
			SoundManager.Instance.PlayExplosionSound();
            
			Destroy(Instantiate(_bloodVFX, transform.position, Quaternion.identity), 3f);
            
			Destroy(gameObject);
		}
	}
	
	protected void Move()
	{
		transform.Translate(Vector2.down * (_movementSpeed * Time.deltaTime));
	}
	
	private float GetRealDamage(float damage)
	{
		int criticalDamageMultiply = Random.Range(0, 100) <= Upgrades.CriticalDamageChance ? 2 : 1;
            
		float realDamage = (damage + Upgrades.IncreaseDamageUpgrade) * criticalDamageMultiply;

		if (Upgrades.ShotsAmount > 0)
		{
			float decreasePercents = 0.2f + (Upgrades.ShotsAmount - 1) * 0.05f;
			realDamage *= 1 - decreasePercents;
		}

		return realDamage;
	}
}