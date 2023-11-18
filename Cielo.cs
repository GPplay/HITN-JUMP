using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cielo : MonoBehaviour
{

    void Update()
    {
        // Obtenemos la posicion de la camara
        Vector3 cameraPosition = GetComponent<Camera>().transform.position;

        // Movemos la imagen a la posicion de la c�mara
        transform.position = cameraPosition;
    }
}
