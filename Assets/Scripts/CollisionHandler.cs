using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    
    bool isConntrolable = true;
    bool isCollidable = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    void Update()
    {
        RespondToDebugKeys();

    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("C key pressed");
            isCollidable = !isCollidable; //Toggle crash ability
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (!isConntrolable || !isCollidable ) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Fuel":
                Debug.Log("Collided with Fuel object.");
                break;
            case "Finish":
                Debug.Log("Collided with Finish object. Level Complete!");
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
             
                break;
        }
    }

    void StartSuccessSequence()
    {
        isConntrolable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movment>().enabled = false; // Disable movement script
        Invoke("LoadNextLevel", delayBeforeReload);
    }

    void StartCrashSequence()
    {   
        isConntrolable = false;
        audioSource.Stop();  
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movment>().enabled = false; // Disable movement script
        Invoke("ReloadLevel", delayBeforeReload);
    }



    void ReloadLevel()
        {
            //todo add fade out sound effect
            int currentScene = SceneManager.GetActiveScene().buildIndex; //Get current scene index
            SceneManager.LoadScene(currentScene); // LoadScene - Can load by index or name(string)
        }
    void LoadNextLevel()  
    {
            //todo add fade out sound effect
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;
            if(nextScene == SceneManager.sceneCountInBuildSettings) //SceneManager.sceneCountInBuildSettings = nunber of total scenes
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
    
}
