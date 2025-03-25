using UnityEngine;

public class RestarButton : MonoBehaviour
{
   public void restart() {
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
   }
}
