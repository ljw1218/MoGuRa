using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Image HelpPanel;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OnClickResume();
    }

    void Update()
    {
        
    }

    public void OnClickPause()
    {
        Time.timeScale = 0f;
        HelpPanel.gameObject.SetActive(true);
        HelpPanel.transform.localScale = Vector3.one;
    }

    public void OnClickResume()
    {
        HelpPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
