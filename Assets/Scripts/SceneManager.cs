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
            UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("VillageScene");
        }
    }
}
