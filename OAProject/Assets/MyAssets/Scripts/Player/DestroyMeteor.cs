using UnityEngine;

public class DestroyMeteor : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;

    private void OnDestroy()
    {
        AudioManager.Instance.PlaySFXOneShot(destroySound);
    }
}
