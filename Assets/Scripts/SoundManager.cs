using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClip[] _explosionSounds;
    [SerializeField] private AudioClip _hitSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosionSound()
    {
        int id = Random.Range(0, 2);
        _audioSource.PlayOneShot(_explosionSounds[id]);
    }

    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitSound);
    }
}