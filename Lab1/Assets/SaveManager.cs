using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    // We will save the Player's position as an example
    public Transform playerTransform;

    public void SaveGame()
    {
        // Save X, Y, and Z coordinates
        PlayerPrefs.SetFloat("PlayerX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", playerTransform.position.z);
        
        // Save which scene we are in
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            // Teleport the player (disable controller first to avoid physics glitches)
            CharacterController cc = playerTransform.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            
            playerTransform.position = new Vector3(x, y, z);
            
            if (cc != null) cc.enabled = true;
            Debug.Log("Game Loaded!");
        }
    }
}