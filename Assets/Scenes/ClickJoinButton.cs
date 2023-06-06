using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ClickJoinButton : MonoBehaviour
{
    public Button JoinButton;
    
    void Start()
    {
        Button btn = JoinButton.GetComponent<Button>();
        btn.onClick.AddListener(goToJoinScene);
        
    }

    void goToJoinScene()
    {
        Debug.Log("Join button clicked");
        SceneManager.LoadScene("Assets/Scenes/JoinGameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
