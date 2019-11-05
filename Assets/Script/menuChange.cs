using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}