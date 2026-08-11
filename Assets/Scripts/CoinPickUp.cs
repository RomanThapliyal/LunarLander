using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    [SerializeField] Animator animator;
   public void DestroyItSelf()
    {
        Debug.Log("Coin deleted");
        Destroy(gameObject);
    }
    private void Start()
    {
        Invoke("PlayCoinAnimation", Random.Range(0f, 2f));
    }

    private void PlayCoinAnimation()
    {
        animator.Play("CoinAnimation");
    }

}
