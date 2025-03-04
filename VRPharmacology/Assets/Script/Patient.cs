using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] GameObject maleModel;
    [SerializeField] GameObject femaleModel;
    public void PatientSwitch(){
        maleModel.SetActive(!maleModel.activeInHierarchy);
        femaleModel.SetActive(!femaleModel.activeInHierarchy);
    }

}
