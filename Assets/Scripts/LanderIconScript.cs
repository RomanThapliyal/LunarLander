using UnityEngine;

public class LanderIconScript : MonoBehaviour
{
    [SerializeField] private float referenceSize = 6f;
    [SerializeField] private float referenceScale = 0.5f;

    void LateUpdate()
    {
        float scale = (Camera.main.orthographicSize / referenceSize) * referenceScale;
        transform.localScale = Vector3.one * scale;
    }
    private void Update()
    {
        if (Lander.Instance.state != Lander.State.waitingToStart)
        {
            gameObject.SetActive(false);
        }
    }
}
