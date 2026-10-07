using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Image helpPanel;
    [SerializeField] private Image resultPanel;
    [SerializeField] private Image selectPanel;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text maxscoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text resultScoreText;
    [SerializeField] private TMP_Text resultComboText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] MoguraSpawner spawner;

    public int combo;
    private int scoreScale;
    private int maxCombo;
    public int score;
    private float timeLeft;
    private bool bIsPlaying;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Init();
        OnClickResume();
        
        UpdateUI();
        spawner.Init(AddScore);
        spawner.StartSpawning();
        bIsPlaying = true;
    }

    private void Init()
    {
        combo = 0;
        maxCombo = 0;
        score = 0;
        scoreScale = 1;
        timeLeft = GameData.gameMaxTime;
        maxscoreText.text = $"{GameData.strBestScoreBase}{GameData.maxScore}";
        Time.timeScale = 1f;
        selectPanel.gameObject.SetActive(false);
        resultPanel.gameObject.SetActive(false);
        helpPanel.gameObject.SetActive(true);
    }
    public void AddScore(int amount)
    {
        if (!bIsPlaying)
            return;

        score += amount*scoreScale;
        if (score < 0)
            score = 0;
        scoreScale = (combo / 10) + 1;
        if (scoreScale > 5)
            scoreScale = 5;
        UpdateUI();
    }
    private void UpdateUI()
    {
        scoreText.text = $"{GameData.strScoreBase}{score}";
        comboText.text = $"{GameData.strComboBase}{combo}";
        if (maxCombo < combo)
            maxCombo = combo;
        if (GameData.maxScore < score)
        {
            maxscoreText.text = $"{GameData.strBestScoreBase}{score}";
            GameData.maxScore = score;
        }
    }
    void Update()
    {
        if (!bIsPlaying)
            return;
        
        timeLeft -= Time.deltaTime;
        timeText.text = $"{GameData.strtimeBase}{(int)(timeLeft)}";
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            EndGame();
        }
    }

    private void EndGame()
    {
        bIsPlaying = false;
        spawner.StopSpawning();
        resultScoreText.text = $"{GameData.strScoreBase}{score}";
        resultComboText.text = $"{GameData.strBestComboBase}{maxCombo}";
        resultPanel.gameObject.SetActive(true);
    }
    public void OnClickPause()
    {
        Time.timeScale = 0f;
        helpPanel.gameObject.SetActive(true);
    }

    public void OnClickResume()
    {
        helpPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickRestart()
    {
        resultPanel.gameObject.SetActive(false);
        helpPanel.gameObject.SetActive(false);
        selectPanel.gameObject.SetActive(true);
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene(GameData.strMainScene);
    }

    public void OnClickEasy()
    {
        GameData.isHard = false;
        SceneManager.LoadScene(GameData.strGameScene);
    }

    public void OnClickHard()
    {
        GameData.isHard = true;
        SceneManager.LoadScene(GameData.strGameScene);
    }
}
