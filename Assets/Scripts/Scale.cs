using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private Transform pPos;
    [SerializeField] private Transform enUpPos;
    [SerializeField] private Transform enDownPos;
    private float yAxis;
    private void Awake()
    {
        yAxis = GlobalS.Size.y;
        Debug.Log("Important "+yAxis);
        if (Debug.isDebugBuild)
        {
            Debug.Log("Camera X Axis Size: " + GlobalS.Size.x + "\nCamera Y Axis Size: " + GlobalS.Size.y);
        }
    }

    private void Update()
    {
        var pos = pPos.position;
        const float change = 1.25f;
        var yUpPos = pos.y + (change * yAxis);
        var yDownPos = pos.y - (change * yAxis);
        enUpPos.position = new Vector3(pos.x,yUpPos,0);
        enDownPos.position = new Vector3(pos.x,yDownPos,0);
    }
}
