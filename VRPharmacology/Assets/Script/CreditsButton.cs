using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject point;
    public void OnButtonPress() {
        mainCamera.transform.LookAt(point.transform);
    }
}
