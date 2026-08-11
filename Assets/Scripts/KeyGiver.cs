using UnityEngine;
using UnityEngine.UI;
public class KeyGiver : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float requiredHoverTime = 2f;
    [SerializeField] private Sprite hudIcon;

    private float hoverTimer;
    private bool landerInside;
    private bool keyGiven;

    private void Update()
    {
        if (!landerInside || keyGiven)
            return;
        hoverTimer += Time.deltaTime;
        fillImage.fillAmount = hoverTimer / requiredHoverTime;
        if (hoverTimer >= requiredHoverTime)
        {
            keyGiven = true;
            Lander.Instance.GiveKey(this);
            SoundManager.Instance.PlayKeyPickUpSound();
            gameObject.SetActive(false);
        }
    }
    public Sprite GetHUDSprite()
    {
        return hudIcon;
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