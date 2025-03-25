using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip hitTakenClip;
    [SerializeField] private AudioClip levelUpClip;

    private void OnEnable()
    {
        PlayerEvents.OnShoot += PlayShootSound;
        PlayerEvents.OnTakeDamage += PlayDamageSound;
        PlayerEvents.OnLevelUp += PlayLevelUpSound;
    }

    private void PlayLevelUpSound()
    {
        audioSource.PlayOneShot(levelUpClip);
    }

    private void PlayDamageSound()
    {
        audioSource.PlayOneShot(hitTakenClip);
    }

    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootClip);
    }

    private void OnDisable()
    {
        PlayerEvents.OnShoot -= PlayShootSound;
        PlayerEvents.OnTakeDamage -= PlayDamageSound;
    }
}
