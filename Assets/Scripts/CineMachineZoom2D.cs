using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CineMachineZoom2D : MonoBehaviour
{
    public const float NORMAL_ORTHOGRAPHIC_SIZE = 14f;
    [SerializeField] CinemachineCamera cinemachineCamera;
    private float targetOrthographicSize=10f;

    public static CineMachineZoom2D Instance { get; private set; }

    private void Awake()
    {
        Instance = this;    
    }
    private void Update()
    {
        float zoomSpeed = 2f;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize,zoomSpeed*Time.deltaTime);
    }
    public void SetTargetOrthoGraphicSize(float targetOrthographicSize)
    {
        this.targetOrthographicSize= targetOrthographicSize;
    }

    public void SetNormalOrthoGraphicSize()
    {
        SetTargetOrthoGraphicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
