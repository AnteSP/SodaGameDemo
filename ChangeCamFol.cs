using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamFol : MonoBehaviour
{
    UnityEngine.UI.Slider s;
    [SerializeField] Movement M;
    Vector2 OG;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<UnityEngine.UI.Slider>();
        OG = M.CameraFollowThreshold;
    }

    public void CHANGE()
    {

        M.CameraFollowThreshold = (OG * (s.value + 0.01f) * 2);

    }

    public void ACCTIME()
    {

        Stats.AccelerateTime(s.value + 0.01f);

    }
}
