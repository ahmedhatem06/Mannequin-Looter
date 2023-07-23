using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardManager : MonoBehaviour
{
    [HideInInspector]
    public List<SecurityGuard> securityGuards = new List<SecurityGuard>();
    private GameObject securityGuardWalkPointsParent;
    public GameObject securityGuardPrefab;

    private void Start()
    {
        //Search for the SecurityGuardWalkPointsParent with SecurityGuardWalkPointsParent tag.
        securityGuardWalkPointsParent = GameObject.FindGameObjectWithTag("SecurityGuardWalkPointsParent");

        if (securityGuardWalkPointsParent == null)
        {
            Debug.LogError("Hatem: Can't find the walkpointsparent, Check if you put tag 'WalkPoints' on the WalkPointsParent");
        }

        InstantiateSecurityGuards();

        AssignSecurityGuardsWalkPoints();
    }

    private void InstantiateSecurityGuards()
    {
        for (int i = 0; i < securityGuardWalkPointsParent.transform.childCount; i++)
        {
            //Instantiate the inspectors.
            GameObject currentInspector = Instantiate(securityGuardPrefab, transform.position, Quaternion.identity);
            //Add them to the list.
            securityGuards.Add(currentInspector.GetComponent<SecurityGuard>());
            EnableRandomSMR(currentInspector);
            currentInspector.name = "Security Guard " + i + 1;
        }
    }

    private void EnableRandomSMR(GameObject inspectorTransform)
    {
        //Get the count of the child count of smr.
        int smrLength = inspectorTransform.GetComponent<SecurityGuard>().smr.transform.childCount;
        //Gets a random out of it.
        int randomSmr = Random.Range(0, smrLength);
        //Enable the random smr.
        inspectorTransform.GetComponent<SecurityGuard>().smr.transform.GetChild(randomSmr).gameObject.SetActive(true);
    }

    private void AssignSecurityGuardsWalkPoints()
    {
        int inspectorIndex = 0;
        foreach (Transform child in securityGuardWalkPointsParent.transform)
        {
            foreach (Transform t in child)
            {
                //Add Inspectors walk points.
                securityGuards[inspectorIndex].GetComponent<SecurityGuard>().WalkPoints.Add(t.gameObject);
                //Make the first walk point as the start position of the Inspector.
                securityGuards[inspectorIndex].transform.position = securityGuards[inspectorIndex].GetComponent<SecurityGuard>().WalkPoints[0].transform.position;
            }
            inspectorIndex++;
        }
    }

}
