using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.king011;
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
    public void OnSwitchChange(Switch evt)
    {
        Debug.Log($"OnSwitchChange {evt.opened}");
    }
    public void OnSwitchChangeCompleted(Switch evt)
    {
        Debug.Log($"OnSwitchChangeCompleted {evt.opened}");
    }
    public void OnButtonSwitchChange(ButtonSwitch evt)
    {
        Debug.Log($"OnButtonSwitchChange {evt.opened}");
    }
}
