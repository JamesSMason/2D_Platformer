using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        int nextLevelID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelID >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelID = 0;
        }
        SceneManager.LoadScene(nextLevelID);
    }
}
