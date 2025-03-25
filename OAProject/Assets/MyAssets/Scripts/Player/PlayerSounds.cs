using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip hitTakenClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerEvents.OnShoot += PlayShootSound;
        PlayerEvents.OnTakeDamage += PlayDamageSound;
    }

    private void PlayDamageSound()
    {
        audioSource.PlayOneShot(hitTakenClip);
    }

    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        PlayerEvents.OnShoot -= PlayShootSound;
        PlayerEvents.OnTakeDamage -= PlayDamageSound;
    }
}
