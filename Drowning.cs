using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drowning : MonoBehaviour
{

    Rigidbody2D rb;
    Collider2D col;

    [SerializeField] Rigidbody2D P;
    [SerializeField] Collider2D Beach;

    Transform water;

    float wFloor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        water = P.transform.Find("Drowning Bar");
        wFloor = water.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.MovePosition(P.position);

        float temp = Mathf.Clamp(Vector2.Distance(rb.position, P.position), 1, 100);
        temp *= temp;

        P.GetComponent<Movement>().SlowDown = (temp);

        water.localPosition = new Vector3(water.localPosition.x, Mathf.Clamp(wFloor + (-wFloor * ( temp - 1 )/40  ),wFloor,0) ,water.localPosition.z);
        water.localScale = new Vector3(1, Mathf.Clamp( 0 + (2 * (temp - 1)/40 ),0,2) , 1);


    }

    
}
