using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : Resource
{
    [SerializeField] Dialogue D;
    [SerializeField] string[] sentences;

    NPCMovement N;
    bool NE = false;

    private void Start()
    {
        if(TryGetComponent<NPCMovement>(out NPCMovement a))
        {
            N = a;
            NE = true;
        }
    }

    public override void Use(float Amount)
    {

        GameObject P = GameObject.FindGameObjectWithTag("Player");
        P.GetComponent<Movement>().ShutDown();

        D.gameObject.SetActive(true);
        D.transform.localPosition = new Vector3(D.transform.localPosition.x, (Camera.main.transform.position.y > P.transform.position.y) ? Mathf.Abs(D.transform.localPosition.y) : -Mathf.Abs(D.transform.localPosition.y), D.transform.localPosition.z);
        D.sentences = sentences;

        if (NE)
        {
            N.enabled = false;
            N.Face(P.GetComponent<Rigidbody2D>().position);
            N.ShutDown();
            D.NextSentence(N);
            
        }
        else
        {
            D.NextSentence();
        }
        

        D.index = (!D.gameObject.activeSelf)  ? - 1 : D.index;

    }
}
