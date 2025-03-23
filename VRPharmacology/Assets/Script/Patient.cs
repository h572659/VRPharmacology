using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] GameObject maleModel;
    [SerializeField] Transform maleHatsNode;
    private GameObject[] maleHats;
    [SerializeField] GameObject femaleModel;
    [SerializeField] Transform femaleHatsNode;
    private GameObject[] femaleHats;
    private GameObject currentHat;
    private int nrOfHats;

    void Awake()
    {
        nrOfHats = maleHatsNode.childCount;
        maleHats = new GameObject[nrOfHats];
        femaleHats = new GameObject[nrOfHats];
        for (int i = 0; i < nrOfHats; i++) {
            maleHats[i] = maleHatsNode.GetChild(i).gameObject;
            femaleHats[i] = femaleHatsNode.GetChild(i).gameObject;
    }
        // Just setting the first male hat as the default option to avoid null exception
        currentHat = maleHats[0];
    }
    public void PatientSwitch(){
        if (!maleModel.activeInHierarchy){
            PatientMan();
        } else {
            PatientWoman();
        }
    }
    public void PatientMan(){
        femaleModel.SetActive(false);
        maleModel.SetActive(true);
        currentHat.SetActive(false);
        currentHat = maleHats[Random.Range(0, maleHats.Length)];
        currentHat.SetActive(true);
    }
       public void PatientWoman(){
        maleModel.SetActive(false);
        femaleModel.SetActive(true);
        currentHat.SetActive(false);
        currentHat = femaleHats[Random.Range(0, femaleHats.Length)];
        currentHat.SetActive(true);
    }

}
