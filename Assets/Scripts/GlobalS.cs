using System.Collections.Generic;
using UnityEngine;

public class GlobalS : MonoBehaviour
{
    private static readonly Camera Cam = Camera.main;
    public static readonly Vector3 Size = Cam.ScreenToWorldPoint(new Vector3(Cam.pixelWidth,Cam.pixelHeight,0));

}
public static class Global 
{
    public static readonly Queue<GameObject> Bullets = new Queue<GameObject>();
    public static readonly Queue<GameObject> ShipsUp = new Queue<GameObject>();
    public static readonly Queue<GameObject> ShipsDown = new Queue<GameObject>();
    public static readonly Queue<GameObject> MissilesUp = new Queue<GameObject>();
    public static readonly Queue<GameObject> MissilesDown = new Queue<GameObject>();
    public static int Score = 0;
    public static int Health = 0;
    public static int FrameCount = 0;

    public static void RecycleEn(GameObject go)
    {
        switch (go.name)
        {
            case "ShipUp":
                ShipsUp.Enqueue(go);
                break;
            case "ShipDown":
                ShipsDown.Enqueue(go);
                break;
            case "MissilesUp":
                MissilesUp.Enqueue(go);
                break;
            case "MissilesDown":
                MissilesDown.Enqueue(go);
                break;
        }
    }
}
