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

    public static void Recycle(GameObject go)
    {
        switch (go.name)
        {
            case "ShipUp":
                Score++;
                ShipsUp.Enqueue(go);
                break;
            case "ShipDown":
                Score++;
                ShipsDown.Enqueue(go);
                break;
            case "MissilesUp":
                Score++;
                MissilesUp.Enqueue(go);
                break;
            case "MissilesDown":
                Score++;
                MissilesDown.Enqueue(go);
                break;
            case "Bullets":
                Bullets.Enqueue(go);
                break;
        }
    }
}
