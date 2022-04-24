using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) // Is the tag of the object we bumped into:
        {
            case "Friendly":
                Debug.Log("This object is Friendly");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // It was written this way to help understanding when returning months later. Quick to see it's the current index being loaded.
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Game starts, current scene is 0. 
        int nextSceneIndex = currentSceneIndex + 1; // Increment to the next scene. Missing scene error without the if statement below.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // Once the final scene equals total scenes in build it resets to the first (scene 0).
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
