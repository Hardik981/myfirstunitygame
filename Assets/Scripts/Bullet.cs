using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private Rigidbody2D _rb;
    private const int Up = 50;
    private Renderer _ren;
    private const float XSize = 0.1f;
    private const float YSize = 0.3f;

    private void Awake ()
    {
		_rb = GetComponent<Rigidbody2D>();
        _ren = GetComponent<Renderer>();  
        Functions2D.RelativeTransform(transform,"scale",XSize,YSize);
    }

    private void Start()
    {
        transform.parent = null;    //For Not to move according to its GrandParent Player/Enemy
    }

    private void OnEnable()
    {
        var transform1 = transform;
        transform1.parent = null;
        Functions2D.RelativeTransform(transform1,"scale",XSize,YSize);
    }

    private void FixedUpdate()
    {
        BulVelF();                           //Apply Velocity to Bullet
    }

    private void Update()
    {
        CheckVisible();
    }

    private void CheckVisible()
    {
        if (!_ren.isVisible)
        {
            GameObject o;
            (o = gameObject).SetActive(false);        //Disable EnemyBullet
            Global.Bullets.Enqueue(o);
        }
    }

    private void BulVelF()
    {
        if (gameObject.name == "Bullet")
            _rb.velocity = transform.up * Up;
        else if (gameObject.name == "EnemyBullet") _rb.velocity = transform.up * -Up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "EnemyBullet")
        {
            switch (other.gameObject.name)
            {
                case "Bullet":
                {
                    GameObject go;
                    GameObject o;
                    (go = gameObject).SetActive(false);
                    Global.Recycle(go);
                    (o = other.gameObject).SetActive(false);
                    Global.Recycle(o);
                    break;
                }
                case "Player":
                {
                    Global.Health++;
                    GameObject o;
                    (o = gameObject).SetActive(false);
                    Global.Recycle(o);
                    break;
                }
            }
        }
    }
    
}