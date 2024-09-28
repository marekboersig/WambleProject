using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadMapScene();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            LoadVillageScene();
        }
    }

    public void LoadMapScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        if (MapGenerator.Instance != null)
        {
            MapGenerator.Instance.gameObject.SetActive(true);
        }
    }

    public void LoadVillageScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VillageScene");
        if (MapGenerator.Instance != null)
        {
            MapGenerator.Instance.gameObject.SetActive(false);
        }
    }
}
