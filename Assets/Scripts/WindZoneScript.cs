using UnityEngine;

public class WindZoneScript : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private Transform arrowTransform;
    private float windSpeed=20f;
    private void Awake()
    {
        arrowTransform = transform.GetChild(0);
        Invoke("PlayCloudAnimation", Random.Range(0f, 2f));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Lander.Instance.SetWindForce(arrowTransform.right*windSpeed);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Lander.Instance.SetWindForce(Vector2.zero);
    }

    private void PlayCloudAnimation()
    {
        animator.Play("cloudAnimation");
    }
}
