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

    public void HighLightArea(bool highlighted)
    {
        if (highlighted)
        {
            highlightedArea.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 1.0f, 0.4f);
        }
        else
        {
            highlightedArea.GetComponent<MeshRenderer>().material.color = Color.clear;
        }
    }

    private void Start()
    {
        meshRenderer.material.color = Color.red;
    }

    public IEnumerator SetActive()
    {
        if (!bActive)
        {
            bActive = true;
            meshRenderer.material.color = Color.green;

            yield return new WaitForSeconds(fMaxActiveTime);

            bActive = false;
            meshRenderer.material.color = Color.red;
        }
    }
}
