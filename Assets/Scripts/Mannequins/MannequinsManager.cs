using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinsManager : MonoBehaviour
{
    private int randomAnimationClip = 0;
    private string mannequinsAnimatorParameter = "PoseIndex";

    public GameObject mannequinPrefab;
    private GameObject mannequinPit;
    private List<GameObject> Mannequins = new();
    // Start is called before the first frame update
    void Start()
    {
        mannequinPit = GameObject.FindGameObjectWithTag("MannequinPit");

        if (mannequinPit == null)
        {
            Debug.LogError("Hatem: Can't find the MannequinPit, Check if you put tag 'MannequinPit' on the MannequinPit");
        }

        for (int i = 1; i < mannequinPit.transform.childCount; i++)
        {
            GameObject currentMannequin = Instantiate(mannequinPrefab, mannequinPit.transform.GetChild(i).transform.position, Quaternion.identity);
            currentMannequin.name = "Mannequin_" + (i - 1);
            EnableRandomSMR(currentMannequin);
            Mannequins.Add(currentMannequin);
        }

        for (int i = 0; i < Mannequins.Count; i++)
        {
            randomAnimationClip = Random.Range(0, Mannequins[i].GetComponent<Animator>().runtimeAnimatorController.animationClips.Length);
            Mannequins[i].GetComponent<Animator>().SetInteger(mannequinsAnimatorParameter, randomAnimationClip);

        }
    }

    private void EnableRandomSMR(GameObject inspectorTransform)
    {
        int smrLength = inspectorTransform.GetComponent<MannequinInfoHolder>().smr.transform.childCount;
        int randomSmr = Random.Range(0, smrLength);
        inspectorTransform.GetComponent<MannequinInfoHolder>().smr.transform.GetChild(randomSmr).gameObject.SetActive(true);
    }
}
