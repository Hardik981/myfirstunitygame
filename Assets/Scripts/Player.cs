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
    private const int Rotate = 15; //Player Rotation Speed

    private readonly Vector2 up = new Vector2(0, 1.25f);
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
        StartCoroutine(UpI());
        StartCoroutine(DownI());
    }

    private void FixedUpdate()
    {
        MotionF();
    }

    private void Update()
    {
        CheckHit();
        CountHealth();
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
        var keyA = Input.GetKey(KeyCode.A);
        var keyD = Input.GetKey(KeyCode.D);
        var keyW = Input.GetKey(KeyCode.W);
        var keyS = Input.GetKey(KeyCode.S);
        if (keyA && keyW)
        {
            _rb.AddRelativeForce(-right);
            _rb.AddRelativeForce(up);
            _sr.sprite = pLeft;
        }
        else if (keyA && keyS)
        {
            _rb.AddRelativeForce(-right);
            _rb.AddRelativeForce(-up);
            _sr.sprite = pLeft;
        }
        else if (keyD && keyW)
        {
            _rb.AddRelativeForce(right);
            _rb.AddRelativeForce(up);
            _sr.sprite = pRight;
        }
        else if (keyD && keyS)
        {
            _rb.AddRelativeForce(right);
            _rb.AddRelativeForce(-up);
            _sr.sprite = pRight;
        }
        else if (keyA) //Run code in loop when key is pressed
        {
            _rb.AddRelativeForce(-right);
            _sr.sprite = pLeft;
        }

        else if (keyD)
        {
            _rb.AddRelativeForce(right);
            _sr.sprite = pRight;
        }

        else if (keyW)
        {
            _rb.AddRelativeForce(up);
            _sr.sprite = mSprite;
        }

        else if (keyS)
        {
            _rb.AddRelativeForce(-up);
            _sr.sprite = mSprite;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
            _rb.AddTorque(Rotate);

        else if (Input.GetKey(KeyCode.RightArrow))
            _rb.AddTorque(-Rotate);
        else
            _sr.sprite = mSprite;
        
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