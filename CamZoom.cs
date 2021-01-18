using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{

    [SerializeField] Camera C;

    // Update is called once per frame
    void Update()
    {

        C.orthographicSize -= Input.mouseScrollDelta.y;
        C.orthographicSize = (C.orthographicSize < 1) ? 1 : C.orthographicSize;
        C.orthographicSize = (C.orthographicSize > 20) ? 20 : C.orthographicSize;

    }

}


