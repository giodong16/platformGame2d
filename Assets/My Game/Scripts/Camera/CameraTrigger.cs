using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public static CameraTrigger Instance;
    public CinemachineVirtualCamera virtualCamera;
    
    public float zoomSpeed = 2f;     // Tốc độ zoom
    public float zoomSize = 4f;
    private Coroutine zoomCoroutine;

    private Coroutine panCoroutine;
    private Transform originalFollowTarget;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        originalFollowTarget = virtualCamera.Follow;
    }

    //ZOOM-------------------------------------------
    public void ZoomCam(float size = 4f)
    {
        // Dừng mọi Coroutine zoom khác để không bị xung đột
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(ZoomCamera(size));
    }

    private IEnumerator ZoomCamera(float targetSize)
    {
        // Lấy kích thước hiện tại của Virtual Camera
        float startSize = virtualCamera.m_Lens.OrthographicSize;

        while (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetSize) > 0.01f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }



    // LIA ----------------------
    public void PanToPosition(Transform targetTransform, float duration, float holdTime = 1f)
    {
        if (panCoroutine != null)
        {
            StopCoroutine(panCoroutine);
        }
        panCoroutine = StartCoroutine(PanCameraSmoothTransition(targetTransform, duration, holdTime));
    }

    private IEnumerator PanCameraSmoothTransition(Transform targetTransform, float duration, float holdTime)
    {
        // Lấy vị trí ban đầu của camera và cố định trục z
        Vector3 startPosition = virtualCamera.transform.position;
        virtualCamera.Follow = null;
        Vector3 targetPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, startPosition.z);
        float elapsedTime = 0f;

        // Chuyển đổi vị trí từ từ trong khoảng thời gian duration
        while (elapsedTime < duration)
        {
            virtualCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        virtualCamera.transform.position = targetPosition;

        // Giữ camera trong thời gian holdTime
        yield return new WaitForSeconds(holdTime);

        // Quay về vị trí ban đầu
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            virtualCamera.transform.position = Vector3.Lerp(targetPosition,startPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // về vị trí ban đầu
        virtualCamera.transform.position = originalFollowTarget.position;
        virtualCamera.Follow = originalFollowTarget;
    }



}
