using UnityEngine;


public enum RatState
{
    Approaching,
    RunAway,
    Flee
}
public class RatController : MonoBehaviour

{
[SerializeField] private ComfortLevelFunction comfortLevelFunction; // Reference to the ComfortLevelFunction script to modify the cat's comfort level


private Vector3 target; // The cat's position
    private readonly float ratSpeed = 2f; // The speed the rat moves towards the cat
    private readonly float ratRunawaySpeed = 6f; // The speed the rat runs away from the cat
    public RatState currentState; // Says if the rat is running away from the cat

    void Start()
    {

        comfortLevelFunction = GameObject.FindWithTag("MainCamera").GetComponent<ComfortLevelFunction>();

        currentState = RatState.Approaching; // The rat starts by approaching the cat
    }

    void Update()
    {
       switch (currentState)
        {
            case RatState.Approaching:
                MoveTowardsTarget();
                ReachedCat();
                break;
            case RatState.RunAway:
                MoveAwayFromTarget();
                break;
            case RatState.Flee:
                // Got away, despawn
                Destroy(gameObject);
                break;
        }

        if(transform.position.x > 15 || transform.position.x < -15 || transform.position.y > 15 || transform.position.y < -15)
            ObjectPoolManager.ReturnToObjectPool(gameObject);

    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target - transform.position).normalized; // The direction the rat moves towards the cat
        transform.localPosition += direction * ratSpeed * Time.deltaTime; // Move the big bad rat towards the innocent cat (WHAT A MONSTER!)
    }

    void MoveAwayFromTarget()
    {
        Vector3 direction = (transform.position - target).normalized; // The direction the rat runs from the cat
        transform.localPosition += direction * ratRunawaySpeed * Time.deltaTime; // Move the big bad rat away from the innocent cat (YEA, AND DON'T COME BACK!)
    }

    void ReachedCat()
    {

        if (Vector3.Distance(transform.position, target) < 0.2f && currentState == RatState.Approaching)
        {
            comfortLevelFunction.GetComfy(-22.5f); // The rat made the cat uncomfortable T-T
            //GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().Shake();
            currentState = RatState.RunAway;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {

            //comfortLevelFunction.GetComfy(-10f); // The rat made the cat uncomfortable T-T
            currentState = RatState.RunAway;

        }

    }

}
