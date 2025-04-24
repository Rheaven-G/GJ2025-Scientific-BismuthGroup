using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    #region Properties

    public static GameManager Instance;

    public int phase2Threshold = 10;
    public int phase3Threshold = 30;
    public float timeToPlay = 40;
    public ProgressBar progressBar;
    public Text score;
    public GameObject gameOverScreen;

    [System.NonSerialized]
    public bool timeIsUp = false;

    int currentPhase = 0;
    int numberOfPeoepleHelped = 0;
    float currentPlayTime = 0;
    float progressTimeScale = 1;

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (progressBar == null)
        {
            progressBar = GetComponentInChildren<ProgressBar>();
        }
        gameOverScreen.SetActive(false);
    }
    void Update()
    {

        if (MetroManager.Instance.state == MetroManager.MetroState.SlowingDown) 
        {
            progressTimeScale -= Time.deltaTime * 0.01f;
        }

        if (MetroManager.Instance.state == MetroManager.MetroState.SpeedingUp)
        {
            progressTimeScale += Time.deltaTime * 0.3f;
        }

        progressTimeScale = Mathf.Clamp(progressTimeScale, 0, 1);

        if (progressBar != null && MetroManager.Instance.state != MetroManager.MetroState.Stopped)
        {
            currentPlayTime += Time.deltaTime * progressTimeScale;
            float timePlayedPresentage = Mathf.InverseLerp(0, timeToPlay, currentPlayTime) * 100;
            progressBar.score = timePlayedPresentage;

            if(currentPlayTime >= timeToPlay)
            {
                timeIsUp = true;
            }
        }
    }
    public void AddPersonHelped()
    {
        numberOfPeoepleHelped++;
        if (numberOfPeoepleHelped < phase2Threshold)
        {
            currentPhase = 0;
        }
        else if (numberOfPeoepleHelped < phase3Threshold)
        {
            currentPhase = 1;
        }
        else
        {
            currentPhase = 2;
        }

    }
    public void GameOver()
    {
        score.text = numberOfPeoepleHelped.ToString();
        gameOverScreen.SetActive(true);
    }

    public int GetCurrentPhase()
    { return currentPhase; }
}
