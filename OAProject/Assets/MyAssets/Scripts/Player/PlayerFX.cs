using System;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Level Up Fx")]
    [SerializeField] private ParticleSystem levelUpFdb;
    [SerializeField] private Transform levelUpPosRef;

    [Header("Damage Fx")]
    [SerializeField] private ParticleSystem damageFdb;
    [SerializeField] private Transform damagePosRef;


    private void OnEnable()
    {
        PlayerEvents.OnTakeDamage += TakeDamageFdb;
        PlayerEvents.OnLevelUp += LevelUpFdb;
    }

    private void LevelUpFdb()
    {
        ParticleSystem currentDamageParticle = Instantiate(levelUpFdb, levelUpPosRef.position, Quaternion.identity);
        currentDamageParticle.transform.SetParent(transform);
        currentDamageParticle.Play();
    }

    public void TakeDamageFdb()
    {
        ParticleSystem currentDamageParticle = Instantiate(damageFdb, damagePosRef.position, Quaternion.identity);
        currentDamageParticle.transform.SetParent(transform);
        currentDamageParticle.Play();
    }

    private void OnDisable()
    {
        PlayerEvents.OnTakeDamage -= TakeDamageFdb;
        PlayerEvents.OnLevelUp -= LevelUpFdb;
        foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
        {
            Destroy(particle.gameObject);
        }
    }
}
