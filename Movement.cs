using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float Speed = 0.25f;
    [SerializeField] float SlowDownCoefficent = 10;
    [SerializeField] float AnimSpeed = 4;
    public Vector2 CameraFollowThreshold = new Vector2(5,2);
    [SerializeField] Vector2 CameraBoundsPos, CameraBoundsNeg;
    float RSpeed;

    public float SlowDown = 1;

    Animator An;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        An = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement stuff
        int Hor = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        int Ver = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

        RSpeed = Input.GetKey(KeyCode.LeftShift) ? Speed / SlowDownCoefficent : Speed;
        RSpeed = Input.GetKey(KeyCode.Space) ? 0 : RSpeed;

        RSpeed *= (1 / SlowDown);

        rb.MovePosition(rb.position + new Vector2(Hor * RSpeed, Ver * RSpeed));

        //Animation stuff
        An.SetInteger("Horizontal", Hor);
        An.SetInteger("Vertical", Hor == 0 ? Ver : 0);

        An.SetFloat("Speed", RSpeed*AnimSpeed);


        Vector3 temp = Camera.main.transform.position;
        //camera stuff
        if (CameraFollowThreshold.x > 0.5f)
        {
            bool toofarx = (Mathf.Abs(transform.position.x - temp.x) > CameraFollowThreshold.x);
            bool toofary = (Mathf.Abs(transform.position.y - temp.y) > CameraFollowThreshold.y);

            float tempx = toofarx ? (transform.position.x - temp.x) * RSpeed / CameraFollowThreshold.x : 0;
            float tempy = toofary ? (transform.position.y - temp.y) * RSpeed / CameraFollowThreshold.y : 0;

            Camera.main.transform.position += new Vector3(tempx, tempy, 0);
            temp = Camera.main.transform.position;
        }
        else
        {
            temp = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        }
        

        Camera.main.transform.position = new Vector3(Mathf.Clamp(temp.x, CameraBoundsNeg.x, CameraBoundsPos.x), Mathf.Clamp(temp.y, CameraBoundsNeg.y, CameraBoundsPos.y), temp.z);
    }

    public void ShutDown()
    {
        An.SetInteger("Horizontal", 0);
        An.SetInteger("Vertical", 0);
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }

    public void Face(Vector2 a)
    {
        a = a - rb.position;

        An.SetInteger("Horizontal", 0);
        An.SetInteger("Vertical", 0);

        if (Mathf.Abs(a.x) > Mathf.Abs(a.y))
        {

            An.Play((a.x < 0) ? "Idle left" : "Idle right");

        }
        else
        {

            An.Play((a.y < 0) ? "Idle down" : "Idle up");

        }
    }



}
