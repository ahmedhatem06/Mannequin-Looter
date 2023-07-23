using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    [HideInInspector]
    public List<NonAITourist> inspectors = new List<NonAITourist>();
    private GameObject inspectorWalkPointsParent;
    private GameObject inspectorLookPointsParent;
    public GameObject inspectorsPrefab;
    private bool canMove;
    private void Awake()
    {
        inspectorWalkPointsParent = GameObject.FindGameObjectWithTag("TouristWalkPointsParent");

        if (inspectorWalkPointsParent == null)
        {
            Debug.LogError("Hatem: Can't find the TouristWalkPointsParent, Check if you put tag 'TouristWalkPointsParent' on the TouristWalkPointsParent");
        }

        inspectorLookPointsParent = GameObject.FindGameObjectWithTag("InspectorLookPointsParent");

        if (inspectorLookPointsParent == null)
        {
            Debug.LogError("Hatem: Can't find the inspectorLookPointsParent, Check if you put tag 'InspectorLookPointsParent' on the InspectorLookPointsParent");
        }

        InstantiateInspectors();

        AssignInspectorsWalkPoints();

        AssignInspectorsLookPoints();
    }

    private void OnEnable()
    {
        Tutorial.instance.tutorialDone += InspectorsStatus;
    }

    private void OnDisable()
    {
        Tutorial.instance.tutorialDone -= InspectorsStatus;
    }

    public void InspectorsStatus()
    {
        canMove = true;
    }

    public void AssignTimersToInspectors(List<TMPro.TextMeshProUGUI> inspectorsTimer)
    {
        for (int i = 0; i < inspectors.Count; i++)
        {
            inspectors[i].TimerText = inspectorsTimer[i];
        }
    }

    private void InstantiateInspectors()
    {
        for (int i = 0; i < inspectorLookPointsParent.transform.childCount; i++)
        {
            //Instantiate the inspectors.
            GameObject currentInspector = Instantiate(inspectorsPrefab, transform.position, Quaternion.identity);
            //Add them to the list.
            inspectors.Add(currentInspector.GetComponent<NonAITourist>());
            EnableRandomSMR(currentInspector);
            currentInspector.name = "Inspector " + i + 1;
        }
    }

    private void AssignInspectorsWalkPoints()
    {
        int inspectorIndex = 0;
        foreach (Transform child in inspectorWalkPointsParent.transform)
        {
            foreach (Transform t in child)
            {
                //Add Inspectors walk points.
                inspectors[inspectorIndex].GetComponent<NonAITourist>().WalkPoints.Add(t.gameObject);
                //Make the first walk point as the start position of the Inspector.
                inspectors[inspectorIndex].transform.position = inspectors[inspectorIndex].GetComponent<NonAITourist>().WalkPoints[0].transform.position;
            }
            inspectorIndex++;
        }
    }

    private void AssignInspectorsLookPoints()
    {
        int inspectorIndex = 0;
        foreach (Transform child in inspectorLookPointsParent.transform)
        {
            foreach (Transform t in child)
            {
                //Add Inspectors look points.
                inspectors[inspectorIndex].GetComponent<NonAITourist>().LookPoints.Add(t.gameObject);
            }
            inspectorIndex++;
        }
    }
    private void EnableRandomSMR(GameObject inspectorTransform)
    {
        //Get the count of the child count of smr.
        int smrLength = inspectorTransform.GetComponent<NonAITourist>().smr.transform.childCount;
        //Gets a random out of it.
        int randomSmr = Random.Range(0, smrLength);
        //Enable the random smr.
        inspectorTransform.GetComponent<NonAITourist>().smr.transform.GetChild(randomSmr).gameObject.SetActive(true);
        //Assign the selectedSMR the same as the random smr (For UI picture).
        inspectorTransform.GetComponent<NonAITourist>().selectedSMR = inspectorTransform.GetComponent<NonAITourist>().smr.transform.GetChild(randomSmr).gameObject;
    }

}
