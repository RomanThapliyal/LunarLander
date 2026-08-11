using TMPro;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private void Awake()
    {
        Destroy(gameObject, 1.5f);
    }
    public void SetTextTo(string text)
    {
        textMeshPro.text = text;
    }
}
