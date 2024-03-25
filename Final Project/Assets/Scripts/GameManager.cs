using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private ScreenFader screenFader;
    public Animator transition;

    public float transitionTime = 3f;

    public GameObject settingsPanel;
    public GameObject GameOverPanel;

    public void Play(){
        Debug.Log("Playing.");
        //SceneManager.LoadScene("BasicMovement");
        //screenFader.FadeToColor("Gameplay");
        LoadNextLevel();
    }

    public void openSettings(){
        Debug.Log("Pulling up Settings Menu now!");
        settingsPanel.SetActive(true);
    }

    public void closeSettings(){
        Debug.Log("Closing Settings Menu.");
        settingsPanel.SetActive(false);
    }
    public void Quit(){
        Application.Quit();
        Debug.Log("Quitting.");
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        GameOverPanel.SetActive(true);
    }

    public void RestartButton(){
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenuButton(){
        SceneManager.LoadScene("Main Menu");
    }
}