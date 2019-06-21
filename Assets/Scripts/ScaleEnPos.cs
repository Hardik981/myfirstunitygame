using UnityEngine;

public class ScaleEnPos : MonoBehaviour
{
    [SerializeField] private Transform pPos;
    [SerializeField] private Transform enUpPos;
    [SerializeField] private Transform enDownPos;
    private float yAxis;
    private void Awake()
    {
        yAxis = GlobalS.Size.y;
        if (Debug.isDebugBuild)
        {
            Debug.Log("Camera X Axis Size: " + GlobalS.Size.x + "\nCamera Y Axis Size: " + GlobalS.Size.y);
        }
    }

    private void Update()
    {
        var pos = pPos.position;
        const float change = 1.5f;
        var yUpPos = pos.y + (change * yAxis);
        var yDownPos = pos.y - (change * yAxis);
        enUpPos.position = new Vector3(pos.x,yUpPos,0);
        enDownPos.position = new Vector3(pos.x,yDownPos,0);
    }
}
