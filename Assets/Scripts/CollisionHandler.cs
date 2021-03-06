using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.0f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;    

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        EnableDebugKeys();
    }

    void EnableDebugKeys()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision (and a bool in general)
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; } // If transitioning stop the below.

        switch (collision.gameObject.tag) // Is the tag of the object we bumped into:
        {
            case "Friendly":
                Debug.Log("This object is Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // It was written this way to help understanding when returning months later. Quick to see it's the current index being loaded.
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() 
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Game starts, current scene is 0. 
        int nextSceneIndex = currentSceneIndex + 1; // Increment to the next scene. Missing scene error without the if statement below.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // Once the final scene equals total scenes in build it resets to the first (scene 0).
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
