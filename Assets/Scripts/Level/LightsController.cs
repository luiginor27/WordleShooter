using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public List<GameObject> lights;

    public Color neutralColor;

    private void Start()
    {
        TurnLightsOn(false);
    }

    public void EndLevel()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().color = neutralColor;
        }
    }

    public void TurnLightsOn(bool on)
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(on);
        }
    }
}
