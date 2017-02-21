using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CompleteGame : MonoBehaviour {

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
