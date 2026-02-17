using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void route_home()
    {
        SceneManager.LoadScene(0);
    }
    public void route_play()
    {
        SceneManager.LoadScene(1);
    }
}
