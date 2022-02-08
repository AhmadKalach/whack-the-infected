using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PlayScene() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GoToGameOverInfected()
    {
        SceneManager.LoadScene("GameOverInfected", LoadSceneMode.Single);
    }

    public void GoToGameOverAbuse()
    {
        SceneManager.LoadScene("GameOverAbuse", LoadSceneMode.Single);
    }

    public void GoToGameWon()
    {
        SceneManager.LoadScene("GameWon", LoadSceneMode.Single);
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }
}
