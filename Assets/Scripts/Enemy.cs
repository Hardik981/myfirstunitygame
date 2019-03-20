using UnityEngine;

public class Enemy : MonoBehaviour {
    
    private Renderer _ren;
    private bool _camVisible;
    private Rigidbody2D _rb;
    private GameObject _pPos;  //Store Player GameObject
    private Transform _pPosT;   //Store Player Transform
    private const int Speed = 10;
    private Vector2 _direction;
    public delegate void EBulT();
    public event EBulT Ebt;  // it start Enemy Bullet
    public delegate void EBulF();
    public event EBulF Ebf;   // it stop Enemy Bullet
    private const float XSize = 0.15f;
    private const float YSize = 0.5f;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ren = GetComponent<Renderer>();
        Functions2D.RelativeTransform(transform,"scale",XSize,YSize);
    }

    private void Start()
    {
        _pPos = GameObject.Find("Player");
        _pPosT = _pPos.transform;
    }

    private void OnEnable()
    {
        _camVisible = false;
        _pPos = GameObject.Find("Player");
        _pPosT = _pPos.transform;
        Functions2D.RelativeTransform(transform,"scale",XSize,YSize);
    }

    private void FixedUpdate()
    {
        switch (gameObject.name)
        {
            case "MissileUp":
            case "MissileDown":
                Functions2D.FollowOtherInRotate(transform,_pPosT);
                Functions2D.RelativeVelocity(transform, _rb, 'y', -Speed);
                break;
            case "ShipUp":
            case "ShipDown":
                Functions2D.FollowOtherInRotate(transform,_pPosT);
                switch (gameObject.name)
                {
                    case "ShipUp":
                        _rb.velocity = new Vector2(0, -Speed);
                        break;
                    case "ShipDown":
                        _rb.velocity = new Vector2(0, Speed);
                        break;
                }
                break;
        }
    }

    private void Update()
    {
        CheckVisible(); //Check Visibility to Camera
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Bullet":
            {
                Global.Score++;
                GameObject gameObject1;
                GameObject o;
                (gameObject1 = gameObject).SetActive(false);    
                Global.Recycle(gameObject1);  
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (gameObject.name != other.gameObject.name)
            {
                GameObject gameObject1;
                GameObject o;
                (gameObject1 = gameObject).SetActive(false);
                Global.Recycle(gameObject1);
                (o = other.gameObject).SetActive(false);
                Global.Recycle(o);
            }
        }
    }

    private void CheckVisible()
    {
        if (_ren.isVisible)
        {
            if (Ebt != null) Ebt();
            _camVisible = true;
        }
        else
        {
            if (Ebf != null) Ebf();
            if (_camVisible) // Run When Enemy is visibled to camera then become invisible to camera
            {
                GameObject o;
                (o = gameObject).SetActive(false);
                Global.Recycle(o);
            }
        }
    }

}