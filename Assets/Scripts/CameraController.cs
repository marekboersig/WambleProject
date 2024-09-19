using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        InitiateCamera();
    }

    private void Update()
    {
        
    }


    void InitiateCamera()
    {
        Vector3 centerPos = new Vector3(200 / 2f, 200 / 2f, -10f);
        transform.position = centerPos;
        Camera.main.orthographicSize = Mathf.Max(200, 200) / (2f * -5f);
    }
}
