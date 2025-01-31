using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public Button restart;
    // Start is called before the first frame update
    void Start()
    {
        restart = GetComponent<Button>();
        restart.onClick.AddListener(ResetScene);
    }

    public void LoadSceneOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSceneTwo()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Score.scoreCount = 0;
        Player.vivo = true;
    }

}
