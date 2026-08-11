using UnityEngine;

public class LowFuelUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        float lowFuelAmount =0.3f;
        if (Lander.Instance.GetFuelAmountNormalized()<= lowFuelAmount)
        {
            Show();
        }
        else { Hide(); }
    }
    private void Show()
    {
        if (!container.gameObject.activeSelf)
        {
            container.gameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        if (container.gameObject.activeSelf)
        {
            container.gameObject.SetActive(false);
        }
    }
}
