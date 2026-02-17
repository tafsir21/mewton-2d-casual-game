using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text gameoverscreen_pointsText;
    [SerializeField] private TMP_Text high_scoreText;

    [SerializeField] private Image[] heartImages; 

    // Setting
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;    


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }



    private void Start()
    {
        if (musicSlider != null && sfxSlider != null)
        {
            musicSlider.value = SoundManager.Instance.musicVolume;
            sfxSlider.value = SoundManager.Instance.sfxVolume;

            musicSlider.onValueChanged.AddListener(OnMusicChanged);
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
        }
    }


    // handle sound
    private void OnMusicChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }

    private void OnSFXChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }



    // handle value
    public void Update_UI()
    {
        if (GameManager.Instance == null) return;
        
        pointsText.text = GameManager.Instance.currentPoints.ToString();
        gameoverscreen_pointsText.text = GameManager.Instance.currentPoints.ToString();
        high_scoreText.text = GameManager.Instance.GetHighScore().ToString();

    
        int lives = GameManager.Instance.playerLives;

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < lives;
        }
    }
}
