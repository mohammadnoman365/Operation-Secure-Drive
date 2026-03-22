using UnityEngine;

public class PlayGame : MonoBehaviour
{

    public GameObject playGame;

    // Function to start the game by hiding the Objective panel
    public void startGame()
    {
        playGame.SetActive(false);
    }
}
