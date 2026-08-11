using UnityEngine;
using UnityEngine.UI;

public class KeyIconUI : MonoBehaviour
{
    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}