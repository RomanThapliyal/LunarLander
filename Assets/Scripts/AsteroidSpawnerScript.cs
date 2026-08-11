 using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    [SerializeField] private AsteroidMoveScript asteroidPrefab;

    [SerializeField] private float minSpawnTime = 50f;
    [SerializeField] private float maxSpawnTime = 100f;

    [SerializeField] private float minSpeed = 15f;
    [SerializeField] private float maxSpeed = 25f;

    [SerializeField] private float minRotationSpeed = 100f;
    [SerializeField] private float maxRotationSpeed = 400f;

    [SerializeField] private float spawnDistance = 50f;

    private Camera mainCamera;
    private float spawnTimer;

    private void Start()
    {
        mainCamera = Camera.main;
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnAsteroid();
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnAsteroid()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Vector2 targetPosition = GetRandomTargetPosition();

        Vector2 moveDirection = (targetPosition - spawnPosition).normalized;

        AsteroidMoveScript asteroid = Instantiate(asteroidPrefab,spawnPosition,Quaternion.identity);

        float speed = Random.Range(minSpeed, maxSpeed);

        float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        if (Random.value < 0.5f)
        {
            rotationSpeed *= -1f;
        }

        asteroid.Initialize(moveDirection,speed,rotationSpeed);
    }
    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0:
                return new Vector2(Random.Range(screenMin.x, screenMax.x),screenMax.y + spawnDistance);

            case 1:
                return new Vector2(Random.Range(screenMin.x, screenMax.x),screenMin.y - spawnDistance);

            case 2:
                return new Vector2(screenMin.x - spawnDistance,Random.Range(screenMin.y, screenMax.y));

            default:
                return new Vector2(screenMax.x + spawnDistance,Random.Range(screenMin.y, screenMax.y));
        }
    }

    private Vector2 GetRandomTargetPosition()
    {
        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
        return new Vector2(Random.Range(screenMin.x, screenMax.x),Random.Range(screenMin.y, screenMax.y));
    }
}
