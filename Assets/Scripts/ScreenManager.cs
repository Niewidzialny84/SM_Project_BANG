using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    private GameObject inputText;
    private GameObject errorText;
    [SerializeField] protected string username;

    public void ChangeScreen(string sceneName)
    {
        switch (sceneName)
        {
            case "Loading Screen":
                inputText = GameObject.FindGameObjectWithTag("Username");

                if (inputText is null)
                {
                    Debug.Log("ERROR: Object not found!");
                }
                //Debug.Log(inputText.GetComponent<TMP_InputField>().text);
                else if (inputText.GetComponent<TMP_InputField>().text != "")
                {
                    errorText = GameObject.FindGameObjectWithTag("ErrorMSG");
                    if (errorText is not null)
                    {
                        errorText.GetComponent<TMP_Text>().text = "";
                    }
                    username = inputText.GetComponent<TMP_InputField>().text;
                    PhotonNetwork.NickName = username;
                    SceneManager.LoadScene(sceneName);
                    break;
                }
                else
                { 
                    Debug.Log("ERROR: No Username!");
                    errorText = GameObject.FindGameObjectWithTag("ErrorMSG");
                    if(errorText is not null)
                    {
                        errorText.GetComponent<TMP_Text>().text = "Missing username";
                    }
                }
                
                break;
            default:
                // code block
                break;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
