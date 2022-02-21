using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject currentLevel;

    public void Retry()
    {
        CamFollow.CanFollow = true;
        Destroy(currentLevel);
        SceneManager.LoadScene(0);
    }
   
}
