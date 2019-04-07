using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Functions2D : MonoBehaviour
{
    private Camera cam;

    public static void FollowOtherInRotate(Transform main, Transform other)
    {  
        var direction = new Vector3();
        if (main)
        {
            var position = main.position;
            if (other)
            {
                var positionOther = other.position;
                direction = new Vector2(
                    position.x - positionOther.x,
                    position.y - positionOther.y
                );
            }
            main.up = direction;
        }
    }

    public static void RelativeVelocity(Transform t,Rigidbody2D rb,char axis,int value)
    {
        var locVel = t.InverseTransformDirection(rb.velocity);
        switch (axis)
        {
            case 'x':
                locVel.x = value;
                break;
            case 'y':
                locVel.y = value; //move at y axis by giving speed
                break;
            default:
                if (Debug.isDebugBuild)
                {
                    Debug.LogError("Invalid axis in Relative Velocity of Functions or upperCase");
                }
                break;
        }
        rb.velocity = t.TransformDirection(locVel); // convert local space direction to world space
    }
    
    public static void RelativeTransform(Transform getT,string type,float x, float y)  // For MultiResolution
    {
        switch (type)
        {
            case "scale":
                getT.localScale = new Vector3(x * GlobalS.Size.x, y * GlobalS.Size.y,0);
                break;
            case "pos":
                getT.position = new Vector3(x * GlobalS.Size.x, y * GlobalS.Size.y,0);
                break;
            case "rot":
                getT.eulerAngles = new Vector3(x * GlobalS.Size.x, y * GlobalS.Size.y,0);
                break;
            default:
                if (Debug.isDebugBuild)
                {
                    Debug.LogError("Invalid type in RelTrans of Functions2D or upperCase");
                }
                break;
        }
        //In Position Vector2, x = -0.9 means left, x = 0.9 means right, y = 0.9 means top, y = -0.9 means down  
    }
    
    public static void CreatePool(Queue<GameObject> list, string goName, GameObject initGo, Transform pos,
        string tagName, float enBulPos, bool wantEnBul, bool wantRandomPos, bool wantYEnBulPos,bool wantTag,bool createBul,ref bool checkNull)
    {
        var randomPos = new Vector3(0, 0, 0);
        if (wantRandomPos)
        {
            var camX = GlobalS.Size.x;
            randomPos = new Vector3(Random.Range(-(0.5f * camX), 0.5f * camX), 0,0); //Random Position of enemy to start
        }

        if (list.Count == 0)
        {
            var position = pos.position;
            var rotation = pos.rotation;
            var go = createBul ? Instantiate(initGo, position, rotation,pos) : Instantiate(initGo, position, rotation);
            go.gameObject.name = goName;
            if (wantTag) go.gameObject.tag = tagName;
            if (wantEnBul)
            {
                var enemyBulletPos = new GameObject("EnemyBulletPos"); //Create EnemyBullet Pos
                enemyBulletPos.transform.parent = go.transform; //only Make Parent not take parent transform properties but it move according to the parent from its own position
                enemyBulletPos.AddComponent<BulletSpawner>(); //Add Script
                enemyBulletPos.transform.localPosition = wantYEnBulPos ? new Vector3(0, enBulPos, 0) : new Vector3(enBulPos, 0, 0);
            }
            go.transform.position = go.transform.position + randomPos; //Make Enemy Position Random
        }
        else //Reuse 
        {
            var poll = list.Dequeue();
            if (createBul)
            {
                if (checkNull)
                {
                    poll.gameObject.name = goName;
                    poll.transform.parent = pos;
                }
                else
                    return;
            }
            poll.transform.position = pos.position + randomPos;
            poll.transform.rotation = pos.rotation;
            poll.SetActive(true);
        }
    }
}


