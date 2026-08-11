using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void OpenGate()
    {
        SoundManager.Instance.PlayGateOpenSound();
        animator.Play("gateopen");
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
}
