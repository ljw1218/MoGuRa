using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Image MainPanel;
    [SerializeField] private Image SelectPanel;
    [SerializeField] private Image HelpPanel;

    void Start()
    {
        MainPanel.gameObject.SetActive(true);
        HelpPanel.gameObject.SetActive(false);
        SelectPanel.gameObject.SetActive(false);
    }

    public void OnClickStart()
    {
        MainPanel.gameObject.SetActive(false);
        SelectPanel.gameObject.SetActive(true);
    }
    
    public void OnClickHelp()
    {
        MainPanel.gameObject.SetActive(false);
        HelpPanel.gameObject.SetActive(true);
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

    public void OnClickExit()
    {
        HelpPanel.gameObject.SetActive(false);
        SelectPanel.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);
    }
}
