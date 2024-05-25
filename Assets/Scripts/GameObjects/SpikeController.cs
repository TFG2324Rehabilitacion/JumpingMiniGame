using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{

    public GameObject[] spikes;


    public void ActivateSpikes()
    {
        foreach(GameObject spike in spikes)
        {
            spike.SetActive(true);
        }
    }

    public void DesactivateSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(false);
        }
    }
}
