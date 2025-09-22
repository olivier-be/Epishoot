using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public string NextScene;

    void Start () {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    
    void OnClick()
    {
        //GetComponent<NetworkLauncher>().JoinOrCreateRoomButton();
        //SceneManager.LoadScene(NextScene);
    }

}
