using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow : MonoBehaviour
{
    public InputField usernameInput;
    public InputField PsdInput;

    public void onLogin()
    {
        string userName = usernameInput.text;
        string psd = PsdInput.text;
        print("username = " + userName);
        print("psd = " + psd);
        if (userName == null || userName.Length < 3
            || psd == null || psd.Length < 3)
        {
            print("用户名或密码有误！");
            return;
        }
        networkCommand.GetInstance().ExcudeCommand("Login", new string[]{userName, psd});
    }

    public void OnQuit()
    {
        Application.Quit();
        print("sdhfkuhsdf");
    }
}
