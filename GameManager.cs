using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentPoints = 0;
    public int playerLives = 3;

    public bool isGameOver = false;
    public bool isPause = false;


    [SerializeField] private Animator AppleAnim;
    [SerializeField] private Animator PlayerAnim;


    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);

        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // point
    public void AddPoint(int amount)
    {
        if (isGameOver) return;

        currentPoints += amount;
        Debug.Log(currentPoints);

        AppleAnim.SetTrigger("isHit");
        SoundManager.Instance.PlayPoint();
    }

    // life
    public void LoseLife(int amount)
    {
        if (isGameOver) return;

        playerLives -= amount;
        // Debug.Log(playerLives);
        SoundManager.Instance.PlayDamage();
    
        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    // save the score
    private void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentPoints > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentPoints);
            PlayerPrefs.Save();

            Debug.Log(highScore);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }



    // handle game state
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        Debug.Log("game over");
        SoundManager.Instance.PlayGameOver();
        SoundManager.Instance.StopMusic();
        PlayerAnim.SetBool("isEnd", true);
        
        SaveHighScore();
        
        StartCoroutine(PauseAfterAnimation());
    }

    public void PasueGame()
    {
        isPause = true;
        Time.timeScale = 0f;    
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1f;    
    }


    // game restart
    public void RestartGame()
    {
        Time.timeScale = 1f;   // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


        
    private IEnumerator PauseAfterAnimation()
    {
        yield return new WaitForSeconds(
            PlayerAnim.GetCurrentAnimatorStateInfo(0).length
        );

        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }


}
