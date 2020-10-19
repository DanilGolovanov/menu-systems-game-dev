using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables

    //public string LoadScene = "GameScene";
   
    public GameObject IWantToDisableThis;

    public OptionsMenu optionsMenu;
    
    #endregion

    public void Start()
    {
        optionsMenu.LoadPlayerPrefs();
    }

    public void LoadSpecificScene(string gameSceneName)
    {
        //Option #1: make fade transition using imported package
        Initiate.Fade(gameSceneName, new Color(0, 0, 0), 2.0f);

        //Option #2: default scene load
        //SceneManager.LoadScene(LoadScene);

        //Options #3: load with load bar (implemented in LevelLoader.cs)
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }


    //public void OnGUI()
    //{
    //    GUI.Box(new Rect(10, 10, 100, 90), "Testing box");
    //    if (GUI.Button(new Rect(20, 40, 80, 20), "Press me"))
    //    {
    //        IWantToDisableThis.SetActive(false);
    //        Debug.Log("Press me button got pressed");
    //    }
    //    if (GUI.Button(new Rect(20, 70, 80, 20), "Press me 2"))
    //    {
    //        Debug.Log("Press me 2 button got pressed");
    //        QuitGame();
    //    }
    //}
}