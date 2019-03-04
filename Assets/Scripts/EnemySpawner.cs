using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject shipUp; //Take Enemy Prefab
    [SerializeField] private GameObject shipDown;
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform enemyInitUpPos; //Take Enemy initialization Position 
    [SerializeField] private Transform enemyInitDownPos;

    private void Start()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Screen Width: " + Screen.width);
            Debug.Log("Screen Height: " + Screen.height);
        }

        StartCoroutine(CreateEnemyF());
    }

    private IEnumerator CreateEnemyF()
    {
        while (true)
        {
            const float bulPos = -0.3f;
            var check = false;
            var ranEn = Random.Range(1f, 5f);
            if (ranEn >= 1 && ranEn < 2)
                Functions2D.CreatePool(Global.ShipsUp, "ShipUp", shipUp, enemyInitUpPos, "Enemy", bulPos, true, true,
                    true, true, false, ref check);
            else if (ranEn >= 2 && ranEn < 3)
                Functions2D.CreatePool(Global.ShipsDown, "ShipDown", shipDown, enemyInitDownPos, "Enemy", bulPos, true,
                    true, true, true, false, ref check);
            else if (ranEn >= 3 && ranEn < 4)
                Functions2D.CreatePool(Global.MissilesUp, "MissileUp", missile, enemyInitUpPos, "Enemy", bulPos, false,
                    true, true, true, false, ref check);
            else if (ranEn >= 4 && ranEn < 5)
                Functions2D.CreatePool(Global.MissilesDown, "MissileDown", missile, enemyInitDownPos, "Enemy", bulPos,
                    false, true, true, true, false, ref check);

            var randomInit = Random.Range(0.3f, 1.0f);
            yield return new WaitForSeconds(randomInit);
        }
    }

}
