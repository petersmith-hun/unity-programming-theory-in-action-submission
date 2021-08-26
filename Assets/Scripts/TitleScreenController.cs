using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Text validationErrorText;

    public void StartGame()
    {
        if (SetPlayerName())
        {
            SceneManager.LoadScene("Scenes/Main");
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    private bool SetPlayerName()
    {
        if (nameInput.text.Length == 0)
        {
            validationErrorText.gameObject.SetActive(true);
            return false;
        }

        GameSession.instance.playerName = nameInput.text;

        return true;
    }
}
