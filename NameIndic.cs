using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameIndic : MonoBehaviour
{
    static TextMeshProUGUI T;

    static float iniX;
    // Start is called before the first frame update
    void Start()
    {
        T = GetComponent<TextMeshProUGUI>();
        iniX = transform.localPosition.x;
    }

    private void Update()
    {
        transform.parent.position = /*new Vector3(Input.mousePosition.x + R.sizeDelta.x,Input.mousePosition.y,Input.mousePosition.z)*/ Input.mousePosition;
    }

    public static void Indicate(string S)
    {

        T.text = S;

        T.transform.localPosition = new Vector3((Screen.width / 2) - Input.mousePosition.x < 0 ? -iniX : iniX, T.transform.localPosition.y, T.transform.localPosition.z);
    }
}
