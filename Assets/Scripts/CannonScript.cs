using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CannonScript : MonoBehaviour
{
    [SerializeField] public float shootingDistance = 12f;
    [SerializeField] private float shootCooldown = 1.5f;
    [SerializeField] private float warningDelay = 1.5f;

    [SerializeField] private Transform target;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private CannonBullet bullet;

    public static CannonScript Instance { get; private set; }

    private float shootTimer;
    private bool targetWasInRange;
    private bool gameover;

    public event EventHandler onShoot;

    private void Awake()
    {
        Instance= this;
    }
    private void Start()
    {
        target=Lander.Instance.transform;
        Lander.Instance.onStateChanged += Lander_onStateChanged;
    }
    private void Lander_onStateChanged(object sender, Lander.onStateChangedEventArgs e)
    {
        if (e.state == Lander.State.gameOver)
        {
            gameover = true;
        }
    }
    private void Update()
    {
        if (gameover)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, target.position);
        bool targetIsInRange = distance <= shootingDistance;
        if (targetIsInRange && !targetWasInRange)
        {
            shootTimer = warningDelay;
        }
        if (targetIsInRange)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                onShoot?.Invoke(this, EventArgs.Empty);
                SoundManager.Instance.PlayCannonFireSound();
                Instantiate(
                    bullet,
                    shootPoint.position,
                    shootPoint.rotation
                );
                shootTimer = shootCooldown;
            }
        }
        targetWasInRange = targetIsInRange;
    }
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = new Color(1f, 0f, 0f, 0.009f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, shootingDistance);
#endif
    }
}