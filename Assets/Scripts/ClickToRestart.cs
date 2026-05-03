using UnityEngine.SceneManagement;
using UnityEngine;


public class ClickToRestart : MonoBehaviour
{

    void Update()
    {
    
        if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {

            SceneManager.LoadScene(0);

        }

    }

}
