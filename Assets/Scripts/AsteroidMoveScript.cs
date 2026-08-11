using UnityEngine;

public class AsteroidMoveScript : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private float rotationSpeed;
    public void Initialize(Vector2 direction, float speed, float rotation)
    {
        moveDirection = direction;
        moveSpeed = speed;
        rotationSpeed = rotation;
    }

    private void Update()
    {
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        Invoke("DestroyItSelf",10f);

    }
    private void DestroyItSelf()
    {
        Destroy(gameObject);
    }
}