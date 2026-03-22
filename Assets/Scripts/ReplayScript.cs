using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayScript : MonoBehaviour
{
    // Function to replay the game
    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
