using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    [SerializeField] private Sprite pLeft; //Player Image when it move to Left
    [SerializeField] private Sprite pRight; //Player Image when it move to Right
    [SerializeField] private Sprite mSprite; //Player Normal Image

    [SerializeField] private Transform pChildT; // Take Player Bullet Position

    public delegate void PBulT();
    public event PBulT Pbt; // it start Player Bullet
    public delegate void PBulF();
    public event PBulF Pbf; // it stop Player Bullet

    private readonly Vector2 touchUp = new Vector2(0, 1);
    private const int Rotate = 25; //Player Rotation Speed

    private readonly Vector2 up = new Vector2(0, 0.5f);
    private readonly Vector2 right = new Vector2(1.25f, 0);

    private readonly float _dis = Screen.height;
    private bool _upControl; //For Up the Player when pointed to Up Button
    private bool _downControl; //For Down the Player when pointed to Down Button
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); //Add RigidBody2d to _rb
        _sr = GetComponent<SpriteRenderer>();
        var transform1 = transform;
        Functions2D.RelativeTransform(transform1,"scale",0.2f,0.3f);
        Functions2D.RelativeTransform(transform1,"pos",0.5f,-0.7f);
    }

    private void Start()
    {
        pChildT.localPosition = new Vector3(0,0.5f,0);
        StartCoroutine(UpI());
        StartCoroutine(DownI());
    }

    private void FixedUpdate()
    {
        _rb.AddRelativeForce(up);
        MotionF();
    }

    private void Update()
    {
        CheckHit();
        CountHealth();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var transform1 = transform;
            transform1.eulerAngles = transform1.forward * 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var transform1 = transform;
            transform1.eulerAngles = transform1.forward * 180;
        }
        if (Input.GetKey(KeyCode.P)) //Pause Game
        {
            Time.timeScale = 0;
        }

        else if (Input.GetKey(KeyCode.R)) //Start Game
        {
            Time.timeScale = 1;
        }  
    }

    private void CheckHit()
    {
        var hit = Physics2D.Raycast(pChildT.position, transform.up, _dis);
        if (hit)
        {
            if (hit.collider.CompareTag("Enemy"))
                if (Pbt != null)
                    Pbt(); //Fire Bullet by Player
                else if (Pbf != null) Pbf(); //Disable Firing Bullet by Player
        }
        else if (Pbf != null) Pbf(); //Disable Firing Bullet by Player
    }

    private void CountHealth()
    {
        if (Global.Health == 1000)
            Destroy(gameObject);
    }

    private void MotionF()
    {
        if(Input.GetKey(KeyCode.A))
            _rb.AddRelativeForce(-right);
        else if(Input.GetKey(KeyCode.D))
            _rb.AddRelativeForce(right);
        if (Input.GetKey(KeyCode.LeftArrow))
            _rb.AddTorque(Rotate);
        else if (Input.GetKey(KeyCode.RightArrow))
            _rb.AddTorque(-Rotate);
    }

    public void UpF()
    {
        _upControl = true;
        _sr.sprite = mSprite;
    }

    private IEnumerator UpI()
    {
        while (true)
        {
            if (_upControl)
                _rb.AddRelativeForce(touchUp);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void UpExitF()
    {
        _upControl = false;
    }

    public void DownF()
    {
        _downControl = true;
        _sr.sprite = mSprite;
    }

    private IEnumerator DownI()
    {
        while (true)
        {
            if (_downControl)
                _rb.AddRelativeForce(-touchUp);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void DownExitF()
    {
        _downControl = false;
    }
    
}