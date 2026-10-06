using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Image MainPanel;
    [SerializeField] private Image SelectPanel;
    [SerializeField] private Image HelpPanel;

    void Start()
    {
        MainPanel.transform.localScale = Vector3.one;
        SelectPanel.transform.localScale = Vector3.zero;
        HelpPanel.gameObject.SetActive(false);
    }

    public void OnClickStart()
    {
        MainPanel.transform.localScale = Vector3.zero;
        SelectPanel.transform.localScale = Vector3.one;
    }
    
    public void OnClickHelp()
    {
        MainPanel.transform.localScale = Vector3.zero;
        HelpPanel.gameObject.SetActive(true);
        HelpPanel.transform.localScale = Vector3.one;
    }

    public void OnClickEasy()
    {
        GameData.selectedMode = GameData.gameMode.Easy;
    }

    public void OnClickHard()
    {
        GameData.selectedMode = GameData.gameMode.Hard;
    }

    public void OnClickExit()
    {
        MainPanel.transform.localScale = Vector3.one;
        SelectPanel.transform.localScale = Vector3.zero;
        HelpPanel.gameObject.SetActive(false);
    }
}
