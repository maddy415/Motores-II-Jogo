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

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Score.scoreCount = 0;
        Player.vivo = true;
    }

}
