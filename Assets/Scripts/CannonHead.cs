using UnityEngine;

public class CannonVisual : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private ParticleSystem CannonShootVFX;

    private CannonScript cannonScript;
    [SerializeField]private  ShootPoint shootPoint;

    private void Start()
    {
        cannonScript = GetComponentInParent<CannonScript>();
        target=Lander.Instance.transform;
        cannonScript.onShoot += Cannon_onShoot;
    }

    private void Cannon_onShoot(object sender, System.EventArgs e)
    {

        cannonScript.GetComponent<Animator>().Play("Recoil", 0, 0f);
        Instantiate(CannonShootVFX, shootPoint.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= cannonScript.shootingDistance)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
