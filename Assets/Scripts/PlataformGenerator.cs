using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plataforma;
    public int numPlataformas = 10;
    public float gapY = 10.0f;

    void Start()
    {
        GenerarPlataformas();
    }

    void GenerarPlataformas()
    {
        float plataformaH = plataforma.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 posicionInicial = transform.position;
        for (int i = 0; i < numPlataformas; i++)
        {
            Vector3 nuevaPos = posicionInicial + new Vector3(0, i * plataformaH * gapY, -5);
            Instantiate(plataforma, nuevaPos, Quaternion.identity);
        }
    }
    // Update is called once per frame
}
