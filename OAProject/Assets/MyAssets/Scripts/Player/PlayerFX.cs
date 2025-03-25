using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem damageFdb;
    [SerializeField] private Transform damagePosRef;
    private ParticleSystem currentDamageParticle;

    private void Start()
    {
        PlayerEvents.OnTakeDamage += TakeDamageFdb;
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
        foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
        {
            Destroy(particle.gameObject);
        }
    }
}
