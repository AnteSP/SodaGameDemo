using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float Speed = 0.25f;
    [SerializeField] float AnimSpeed = 4;
    int animX, animY;

    public enum NPCType {

        Wander, Path , Still

    }

    public NPCType NPCMtype;

    [SerializeField] Transform Path;
    List<Vector2> PPoints = new List<Vector2>();
    int index;

    Animator An;

    bool WMove;
    [Tooltip("Higher number = less likely to stop/go")] [SerializeField] float WanderStop, WanderGo;
    Vector2 StoredW;

    float Hor = 0, Ver = 0, mult = 0;
    bool Hgreater = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        An = GetComponent<Animator>();
        try
        {
            for (int i = 0; i < Path.childCount; i++)
            {
                PPoints.Add(Path.GetChild(i).transform.position);
            }
        } catch(System.Exception e)
        {

            print(e);
        }
        

    }

    // Update is called once per frame
    void Update()
    {


        Hor = 0;
        Ver = 0;
        mult = 0;
        Hgreater = false;
        animX = 0;
        animY = 0;

        //movement stuff
        if (NPCMtype == NPCType.Path)
        {
            index = (Vector2.Distance(transform.position, PPoints[index]) < 0.2f) ? (index == (PPoints.Count - 1) ? 0 : index + 1) : index;

            Hor = PPoints[index].x - transform.position.x;
            Ver = PPoints[index].y - transform.position.y;

            HandleAnimVals();

            rb.MovePosition(rb.position + new Vector2(Hor * Speed, Ver * Speed));

            //Animation stuff
            An.SetInteger("Horizontal", Hgreater ? (Hor > 0 ? 1 : -1) : 0);
            An.SetInteger("Vertical", Hgreater ? 0 : (Ver > 0 ? 1 : -1));
            An.SetFloat("Speed", Speed * AnimSpeed);

        } else if(NPCMtype == NPCType.Wander)
        {

            if(Random.Range(1f, (1f/Time.deltaTime)*(WMove ? WanderStop : WanderGo) ) < 2)
            {

                WMove = !WMove;
                StoredW = new Vector2(Random.Range(1, 3) * ((Random.Range(1,2) == 1) ? -1:1), Random.Range(1, 3) * ((Random.Range(1, 2) == 1) ? -1 : 1));

            }

            An.SetInteger("Horizontal", 0);
            An.SetInteger("Vertical", 0);
            if (WMove)
            {
                Hor = StoredW.x;
                Ver = StoredW.y;
                HandleAnimVals();

                rb.MovePosition(rb.position + new Vector2(Hor * Speed, Ver * Speed));
                An.SetInteger("Horizontal", Hgreater ? (Hor > 0 ? 1 : -1) : 0);
                An.SetInteger("Vertical", Hgreater ? 0 : (Ver > 0 ? 1 : -1));
            }
            //Animation stuff
            An.SetFloat("Speed", Speed * AnimSpeed);

        }

        rb.velocity = Vector2.zero;
        

    }

    void HandleAnimVals()
    {
        Hgreater = Mathf.Abs(Hor) > Mathf.Abs(Ver);
        mult = (Hgreater) ? 1f / Mathf.Abs(Hor) : 1f / Mathf.Abs(Ver);
        mult = (float.IsNaN(mult) ? 1 : mult);

        Hor *= mult;
        Ver *= mult;

        animX = Hgreater ? (Hor > 0 ? 1 : -1) : 0;
        animY = Hgreater ? 0 : (Ver > 0 ? 1 : -1);
    }

    public void Face(Vector2 a)
    {
        a = a - rb.position;

        An.SetInteger("Horizontal", 0);
        An.SetInteger("Vertical",0);

        if (Mathf.Abs(a.x) > Mathf.Abs(a.y))
        {

            An.Play((a.x < 0) ? "Idle left" : "Idle right");

        }
        else
        {

            An.Play((a.y < 0) ? "Idle down" : "Idle up");

        }
    }

    public void Face(string leftrightdownup)
    {
        An.Play("Idle " + leftrightdownup);
    }

    public void ShutDown()
    {
        An.SetInteger("Horizontal", 0);
        An.SetInteger("Vertical", 0);
        rb.velocity = Vector2.zero;
        this.enabled = false;


    }
}
