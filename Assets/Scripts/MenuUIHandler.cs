using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerName;
    
    private void Start()
    {
        if(GameManager.Instance != null)
        {
            playerName.text = GameManager.Instance.playerName;
        }
    }

    public void StartNew()
    {
        GetPlayerName();
        GameManager.Instance.isGameActive = true;
        GameManager.Instance.LoadData();
        SceneManager.LoadScene(1);
        GameManager.Instance.LoadData();
    }
    
    public void Exit()
    {
                
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

    public void GetPlayerName()
    {
        string name = playerName.text;
        GameManager.Instance.playerName = name;
    }
}
