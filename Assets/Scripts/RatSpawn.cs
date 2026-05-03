using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RatSpawn : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private ComfortLevelFunction comfortLevelFunction;

    public int currentEnemySpawnSpeed = 0;
    public int targetEnemySpawnSpeed = 0;

    private int randDirection;
    private float randTimer;

    public GameObject ratPrefab;
    public float spawnInterval = 5f;
    private float angle;

    private bool shouldGiveBreak = false;
    
    void Start()
    {

        playerController = GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>();

        StartCoroutine(SpawnRandomRats());
        StartCoroutine(ManageRatSpawnSpeed());

    }

    void SpawnRat(int anchor) 
    {

        switch(anchor)
        {

            case 1:

                angle = 0f;

                break;
            case 0:

                angle = -45f;

                break;
            case 7:

                angle = -90f;

                break;
            case 6:

                angle = -135f;

                break;
            case 5:

                angle = -180f;

                break;
            case 4:

                angle = -225f;

                break;
            case 3:

                angle = -270f;

                break;
            case 2:

                angle = -315f;

                break;

        }


        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        GameObject rat = ObjectPoolManager.SpawnObject(ratPrefab, new Vector3(0f, gameObject.transform.localPosition.y + 10f, 0f), Quaternion.identity);
        rat.transform.SetParent(transform);
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        rat.transform.SetParent(null);
        //ObjectPoolManager.SpawnObject(ratPrefab, new Vector3(0f, gameObject.transform.localPosition.y + 10f, 0f), Quaternion.identity);
    }

    IEnumerator Spawn5RatsInARow()
    {

        SpawnRat(0);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(1);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(2);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(3);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(4);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(5);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(6);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(7);

        SpawnRat(0);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(1);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(2);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(3);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(4);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(5);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(6);
        yield return new WaitForSeconds(0.5f);
        SpawnRat(7);

    }


    IEnumerator SpawnRandomRats()
    {

        while (true)
        {

            if (comfortLevelFunction.comfortLevel > 0)
            {

                if (shouldGiveBreak)
                {

                    shouldGiveBreak = false;
                    yield return new WaitForSeconds(5);

                }

                if (currentEnemySpawnSpeed == 0)
                {

                    randDirection = Random.Range(0, 8);
                    randTimer = Random.Range(1f, 3f);

                }
                else if (currentEnemySpawnSpeed == 1)
                {

                    randDirection = Random.Range(0, 8);
                    randTimer = Random.Range(0.5f, 2f);

                }
                else if (currentEnemySpawnSpeed == 2)
                {

                    randDirection = Random.Range(0, 8);
                    randTimer = Random.Range(0.25f, 1f);

                }
                else if (currentEnemySpawnSpeed == 3)
                {

                    randDirection = Random.Range(0, 8);
                    randTimer = Random.Range(0.1f, 0.2f);

                }

                SpawnRat(randDirection);

                yield return new WaitForSeconds(randTimer);

            }

            yield return null;

        }

    }

    IEnumerator ManageRatSpawnSpeed()
    {

        currentEnemySpawnSpeed = 0;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;
        currentEnemySpawnSpeed++;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;
        currentEnemySpawnSpeed++;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;
        currentEnemySpawnSpeed++;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;
        yield return new WaitForSeconds(10);
        shouldGiveBreak = true;


    }

}
