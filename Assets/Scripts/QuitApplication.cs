using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        OnApplicationQuit();
        
    }
    private void OnApplicationQuit()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
        Debug.Log("Application is quitting!");
        Application.Quit();
        
        }       
    }
}
