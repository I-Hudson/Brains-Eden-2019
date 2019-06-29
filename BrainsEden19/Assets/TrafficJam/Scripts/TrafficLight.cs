using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public int iPlayerIndex = 0;

    [SerializeField]
    private float fMaxActiveTime = 3.0f;

    public bool bActive = false;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private GameObject highlightedArea;

    [SerializeField]
    private Material clearMat;

    [SerializeField]
    private Material whiteMat;

    [SerializeField]
    private Material trafflightGreen;
    [SerializeField]
    private Material trafflightRed;

    public void HighLightArea(bool highlighted)
    {
        if (highlighted)
        {
            highlightedArea.GetComponent<MeshRenderer>().material = whiteMat;
        }
        else
        {
            highlightedArea.GetComponent<MeshRenderer>().material = clearMat;
        }
    }

    private void Start()
    {
        meshRenderer.material = trafflightRed;
    }

    public IEnumerator SetActive()
    {
        if (!bActive)
        {
            bActive = true;
            meshRenderer.material = trafflightGreen;

            yield return new WaitForSeconds(fMaxActiveTime);

            bActive = false;
            meshRenderer.material = trafflightRed;
        }
    }
}
