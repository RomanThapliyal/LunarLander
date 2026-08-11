using UnityEngine;
using UnityEngine.UI;

public class KeyReceiver : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float requiredHoverTime = 2f;
    [SerializeField] private KeyGiver requiredKey;

    private float hoverTimer;
    private bool landerInside;
    private bool gateOpened;

    private void Update()
    {
        if (!landerInside || gateOpened)
        {
            return;
        }

        if (!Lander.Instance.HasKey(requiredKey))
        {
            hoverTimer = 0f;
            return;
        }
        hoverTimer += Time.deltaTime;
        fillImage.fillAmount = hoverTimer / requiredHoverTime;
        if (hoverTimer >= requiredHoverTime)
        {
            gateOpened = true;

            Lander.Instance.UseKey(requiredKey);

            GetComponentInParent<GateScript>().OpenGate();

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Lander>() != null)
        {
            landerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Lander>() != null)
        {
            landerInside = false;
            hoverTimer = 0f;
            fillImage.fillAmount = 0;
        }
    }
}
