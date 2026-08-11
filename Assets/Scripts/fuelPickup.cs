using Unity.VisualScripting;
using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        Invoke("PlayFuelPickUpAnimation", Random.Range(0f, 2f));
    }

    private void PlayFuelPickUpAnimation()
    {
        animator.Play("FuelpickupAnimation");
    }
}
