using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public MapSettings mapSettings;
    public List<float> zoomLevels = new List<float> {4f, 8f, 12f, 16f, 20f, 30f, 40f};
    private int currentZoomIndex;

    private Camera cam;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
        currentZoomIndex = zoomLevels.Count / 2;
        InitializeCamera();
        UpdateCameraZoom();
    }

    void Update()
    {
        HandleDragAndDrop();
        HandleZoom();
    }

    void InitializeCamera(Vector3? position = null, int? zoomLevel = null)
    {
        if (position.HasValue)
        {
            transform.position = position.Value;
        } 
        else
        {
            Vector3 centerPos = new Vector3(mapSettings.mapWidth / 2f, mapSettings.mapHeight / 2f, -10f);
            transform.position = centerPos;
        }

        if (zoomLevel.HasValue)
        {
            cam.orthographicSize = zoomLevels[zoomLevel.Value];
            currentZoomIndex = zoomLevel.Value;
        } 
        else
        {
            UpdateCameraZoom();
        }

        ClampCamera();
    }

    /// <summary>
    /// Move the camera position according to drag&drop movement of mouse.
    /// </summary>
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
            ClampCamera();
        }
    }

    /// <summary>
    /// On activation of scroll wheel, increment / decrement the zoom level.
    /// </summary>
    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            if (scrollInput > 0 && currentZoomIndex > 0)
                currentZoomIndex--;
            else if (scrollInput < 0 && currentZoomIndex < zoomLevels.Count - 1)
                currentZoomIndex++;

            UpdateCameraZoom();
        }
    }

    /// <summary>
    /// Set the camera zoom to the current zoom level.
    /// </summary>
    void UpdateCameraZoom() 
    {
        float targetZoom = zoomLevels[currentZoomIndex];
        cam.orthographicSize = targetZoom;

        ClampCamera();
    }

    /// <summary>
    /// Clamp the camera to the map dimensions.
    /// </summary>
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