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
                Debug.Log("Finish Line Reached");
                break;
            case "Fuel":
                Debug.Log("You picked up Fuel");
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
}
