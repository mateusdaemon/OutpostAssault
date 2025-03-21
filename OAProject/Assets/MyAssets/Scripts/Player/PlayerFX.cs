using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private GameObject damageFdb;

    private void Start()
    {
        PlayerEvents.OnTakeDamage += TakeDamageFdb;
    }

    public void TakeDamageFdb()
    {
        damageFdb.SetActive(true);
    }
}
