using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleter : MonoBehaviour
{
   private const string MainMenuSceneName = "MainMenu";
   
   private void OnTriggerEnter(Collider other)
   {
      SceneManager.LoadScene(MainMenuSceneName);
   }
}
