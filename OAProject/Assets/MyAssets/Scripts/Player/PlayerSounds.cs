using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerEvents.OnShoot += PlayShootSound;
    }

    private void PlayShootSound()
    {
        audioSource.clip = shootClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
