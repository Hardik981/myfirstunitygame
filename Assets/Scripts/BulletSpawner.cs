using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private GameObject _bullet;                  // Store Bullet Prefab
    private bool _pBulLoop;             // Start/Stop Player Bullets Loop
    private GameObject _go;                     // Store GameObject (Its gameobject parent)
    private Player _pS;                         // Store Player Script
    private bool _isGameObjectNotNull;     // Check gameObject is null or not          
    private Enemy _enS;
    private bool _enBulLoop;          // Start/Stop Enemy Bullets Loop

    private void Awake()
    {
        _bullet = Resources.Load<GameObject>("Bullet");
        _isGameObjectNotNull = gameObject != null;
    }

    private void Start()
    {
        switch (gameObject.name)
        {
            case "BulletPos":
            {
                _go = gameObject.transform.parent.gameObject; // Store Player GameObject (This gameobject parent)
                _pS = _go.GetComponent<Player>();
                if (_pS != null)
                {
                    _pS.Pbt += StartPBul; //Subscribe event for function
                    _pS.Pbf += StopPBul;
                }

                StartCoroutine(CreateP_Bullets());
                break;
            }
            case "EnemyBulletPos":
            {
                _go = gameObject.transform.parent.gameObject; // Store Enemy GameObject (This gameobject parent)
                _enS = _go.GetComponent<Enemy>();
                if (_enS != null)
                {
                    _enS.Ebt += StartEnBul; //Subscribe event for function
                    _enS.Ebf += StopEnBul;
                }

                StartCoroutine(CreateEn_Bullets());
                break;
            }
        }
    }

    private void OnEnable()
    {
        if (gameObject.name == "EnemyBulletPos")
        {
            if (_enS != null)
            {
                _enS.Ebt += StartEnBul;   //Subscribe event for function
                _enS.Ebf += StopEnBul;
            }
            _enBulLoop = false;
            StartCoroutine(CreateEn_Bullets());
        }
    }

    private void OnDestroy()
    {
        switch (gameObject.name)
        {
            case "BulletPos":
            {
                if (_pS != null)
                {
                    _pS.Pbt -= StartPBul; //UnSubscribe event for function
                    _pS.Pbf -= StopPBul;
                }

                break;
            }
            case "EnemyBulletPos":
            {
                if (_enS != null)
                {
                    _enS.Ebt -= StartEnBul;    //UnSubscribe event for function
                    _enS.Ebf -= StopEnBul;
                }

                break;
            }
        }
    }

    private void OnDisable()
    {
        if (gameObject.name == "EnemyBulletPos")
        {
            if (_enS != null)
            {
                _enS.Ebt -= StartEnBul;     //UnSubscribe event for function
                _enS.Ebf -= StopEnBul;
            }
        }
    }

    private void StartPBul()
    {
        _pBulLoop = true;           //Start Player Bullet
    }

    private void StopPBul()
    {
        _pBulLoop = false;          //Stop Player Bullet
    }

    private void StartEnBul()
    {
        _enBulLoop = true;      //Start Enemy Bullet
    }
    private void StopEnBul()
    {
        _enBulLoop = false;      //Stop Enemy Bullet
    }

    private IEnumerator CreateP_Bullets()
    {
        while (true)
        {
            if(_pBulLoop)
            {
                var o = gameObject;
                Functions2D.CreatePool(Global.Bullets, "Bullet", _bullet, o.transform, "Enemy", 0, false, false,
                    false, true, true, ref _isGameObjectNotNull);
                yield return new WaitForSeconds(0.3f);      //After fire bullet stop for specified time
            }
            yield return new WaitForSeconds(0.001f);       //Always Running
        }
    }

    private IEnumerator CreateEn_Bullets()
    {
        while (true)
        {
            if (_enBulLoop)
            {
                var o = gameObject;
                Functions2D.CreatePool(Global.Bullets, "EnemyBullet", _bullet, o.transform, "Enemy", 0, false, false,
                    false, true, true, ref _isGameObjectNotNull);
                yield return new WaitForSeconds(0.3f); //After fire bullet stop for specified time
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    /*private void CreateBul()
    {
        if (Global.Bullets.Count == 0)  //Create new Bullet
        {
            if (_bullet)
            {
                var goT = gameObject.transform;
                var bul = Instantiate(_bullet, goT.position, goT.rotation, goT);
                bul.gameObject.name = gameObject.name == "BulletPos" ? "Bullet" : "EnemyBullet";
            }
        }
        else     //Reuse Bullet
        {
            var bulPoll = Global.Bullets.Dequeue();
            if (_isGameObjectNotNull)
            {
                var go = gameObject;
                bulPoll.gameObject.name = go.name == "BulletPos" ? "Bullet" : "EnemyBullet";
                bulPoll.transform.position = go.transform.position;
                bulPoll.transform.rotation = go.transform.rotation;
                bulPoll.transform.parent = go.transform;
                bulPoll.SetActive(true);
            }
        }
    }*/
}