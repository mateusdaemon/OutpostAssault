using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem damageFdb;

    private void Start()
    {
        PlayerEvents.OnTakeDamage += TakeDamageFdb;
    }

    public void TakeDamageFdb()
    {
        damageFdb.Play();
    }
}
