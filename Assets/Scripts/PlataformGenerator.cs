using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plataforma;
    public GameObject spike;
    public int numPlataformas;
    public float gapY = 10.0f;

    private List<GameObject> generatePlatforms = new List<GameObject>();

    private bool regenerated = false;

    void Start()
    {
        numPlataformas = GameManager.Instance.maxJumps;
        GenerarPlataformas();
    }

    private void Update()
    {
        if (!GameManager.Instance.playing && !GameManager.Instance.GameFinished() && !regenerated)
        {
            RegenerarPlataformas();
        }
        else if (!regenerated)
        {
            regenerated = true;
        }
    }

    private void GenerarPlataformas()
    {
        float plataformaH = plataforma.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 posicionInicial = transform.position;
        for (int i = 0; i < numPlataformas; i++)
        {
            Vector3 nuevaPos = posicionInicial + new Vector3(0, i * plataformaH * gapY, 0);
            GameObject newPlatform = Instantiate(plataforma, nuevaPos, Quaternion.identity);
            generatePlatforms.Add(newPlatform);

            //Instantiate spike for the platforms
            Vector3 spikePosition = newPlatform.transform.position + new Vector3(0, 0.3f, 0);
            GameObject newSpike = Instantiate(spike, spikePosition, Quaternion.identity);
            //newSpike.transform.SetParent(newPlatform.transform);
            newSpike.SetActive(false);

            SpikeController spikeController = newPlatform.GetComponent<SpikeController>();
            spikeController.spikes = new GameObject[] { newSpike };
        }
    }

    void RegenerarPlataformas()
    {
        foreach (GameObject platform in generatePlatforms)
        {
            Destroy(platform);
        }

        generatePlatforms.Clear();

        GenerarPlataformas();

        regenerated = true;
    }
}
