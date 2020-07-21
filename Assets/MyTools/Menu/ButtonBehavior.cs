using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public string levelname;

    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelname, LoadSceneMode.Single);
    }
}
