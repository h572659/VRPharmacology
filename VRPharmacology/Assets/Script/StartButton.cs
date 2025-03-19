using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
   [SerializeField] GameObject loadingScreen;
 public void NextScene() {
   loadingScreen.SetActive(true);
   SceneManager.LoadScene(1);
 }

}
