using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private Transform fxReference;
    [SerializeField] private ParticleSystem damageFdb;

    private void Start()
    {
        PlayerEvents.OnTakeDamage += TakeDamageFdb;
    }

    public void TakeDamageFdb()
    {
        Instantiate(damageFdb, fxReference.position, Quaternion.identity);
    }
}
