using UnityEngine.SceneManagement;
using UnityEngine;


public class ClickToRestart : MonoBehaviour
{

    void Update()
    {
    
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {

            SceneManager.LoadScene(0);

        }

    }

}
