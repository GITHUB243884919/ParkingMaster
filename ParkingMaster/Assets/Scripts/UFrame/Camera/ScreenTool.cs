using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTool : MonoBehaviour
{
    public Vector3 wP0;
    public Vector3 wP1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalcCorner();
    }

    public void CalcCorner()
    {
        var sP0 = Vector2.zero;
        var sP1 = Vector2.zero;
        sP1.x = Screen.width;
        sP1.y = Screen.height;


        Ray raySp0 = Camera.main.ScreenPointToRay(sP0);
        wP0 = UFrame.Math_F.GetIntersectWithLineAndGround(raySp0.origin, raySp0.direction);

        Ray raySp1 = Camera.main.ScreenPointToRay(sP1);
        wP1 = UFrame.Math_F.GetIntersectWithLineAndGround(raySp1.origin, raySp1.direction);

    }

    public static Vector3 GetBottomCenter()
    {
        var p = Vector3.zero;
        p.x = Screen.width / 2;
        p.y = 0;
        Ray ray = Camera.main.ScreenPointToRay(p);

        return UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
    }
}
