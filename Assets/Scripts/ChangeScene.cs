using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string destinationSceneName;

    public void Go()
    {
        SceneManager.LoadScene(destinationSceneName);
    }
}
