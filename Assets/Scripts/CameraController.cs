using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public MapSettings mapSettings;

    // public float zoomStepDuration = 0.0f;
    public List<float> zoomLevels = new List<float> {4f, 8f, 12f, 16f, 20f, 30f, 40f};
    // public float dragSpeed = 2f;

    private Vector3 dragOrigin;
    private bool isDragging = false;
    private Camera cam;
    private int currentZoomIndex;
    private float zoomVelocity;

    void Start()
    {
        cam = Camera.main;
        currentZoomIndex = zoomLevels.Count / 2;
        InitiateCamera();
    }

    private void Update()
    {
        HandleDragAndDrop();
        HandleZoom();
        UpdateCameraSize();
    }


    void InitiateCamera()
    {
        Vector3 centerPos = new Vector3(mapSettings.mapWidth / 2f, mapSettings.mapHeight / 2f, -10f);
        transform.position = centerPos;
        UpdateCameraSize();
    }

    void HandleDragAndDrop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }

        ClampCamera();
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            if (scrollInput > 0 && currentZoomIndex > 0)
            {
                currentZoomIndex--;
            }
            else if (scrollInput < 0 && currentZoomIndex < zoomLevels.Count - 1)
            {
                currentZoomIndex++;
            }
        }
    }

    void UpdateCameraSize() 
    {
        float targetZoom = zoomLevels[currentZoomIndex];
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, 0);
        ClampCamera();
    }

    void ClampCamera() 
    {
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = camWidth;
        float maxX = mapSettings.mapWidth - camWidth;
        float minY = camHeight;
        float maxY = mapSettings.mapHeight - camHeight;

        float newX = Mathf.Clamp(cam.transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(cam.transform.position.y, minY, maxY);

        cam.transform.position = new Vector3(newX, newY, cam.transform.position.z);
    }
}
