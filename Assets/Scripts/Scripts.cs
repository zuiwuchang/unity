using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scripts : MonoBehaviour
{
    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void GoA()
    {
        SceneManager.LoadScene("A");
    }
}
