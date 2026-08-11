using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 12f;
    [SerializeField] private Rigidbody2D bulletRigidBody;
    [SerializeField] private GameObject bulletExplosinVfx;
    private void Awake()
    {
        bulletRigidBody.linearVelocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3f);
    }

    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<terrain>(out _))
        {
            DestroyItSelf();
        }
        else if (collision.TryGetComponent<CannonBullet>(out _))
        {
            Instantiate(bulletExplosinVfx, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayCannonFireSound();
            DestroyItSelf();
        }
    }
}