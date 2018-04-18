// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//--------------------------------------------------------------------------------------
// Restart object. Inheriting from MonoBehaviour. used for the restart menu buttons
//--------------------------------------------------------------------------------------
public class Restart : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    // RestartGame: Function to reload the current scene on button press
    //--------------------------------------------------------------------------------------
    public void RestartGame()
    {
        // reload the scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    //--------------------------------------------------------------------------------------
    // ReturnToMenu: Function to return to the menu scene on button press
    //--------------------------------------------------------------------------------------
    public void ReturnToMenu()
    {
        // Load the menu scene 
        SceneManager.LoadScene(0);
    }
}