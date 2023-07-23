using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InspectorsInfoManager : MonoBehaviour
{
    public InspectorManager inspectorManager;
    public GameObject allInspectors;
    public GameObject inspectorsPrefab;
    public Transform infoContent;
    private List<TMPro.TextMeshProUGUI> inspectorsTimer = new List<TMPro.TextMeshProUGUI>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inspectorManager.inspectors.Count; i++)
        {
            GameObject tmp = Instantiate(inspectorsPrefab);
            tmp.transform.SetParent(infoContent);
            tmp.transform.localScale = new Vector3(1, 1, 1);

            AssignInspectorsSprites(inspectorManager.inspectors[i].transform, tmp);

            if (tmp.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                inspectorsTimer.Add(tmp.GetComponentInChildren<TMPro.TextMeshProUGUI>());
            }
        }
        inspectorManager.AssignTimersToInspectors(inspectorsTimer);
    }

    private void AssignInspectorsSprites(Transform inspector, GameObject inspectorsInfo)
    {
        inspectorsInfo.GetComponentInChildren<Image>().sprite = inspector.GetComponent<NonAITourist>().selectedSMR.GetComponent<InspectorSpriteHolder>().inspectorSprite;
    }
}
