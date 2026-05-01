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


private Vector2 target; // The cat's position
    private readonly float ratSpeed = 2f; // The speed the rat moves towards the cat
    private readonly float ratRunawaySpeed = 6f; // The speed the rat runs away from the cat
    private RatState currentstate = false; // Says if the rat is running away from the cat

    void Start()
    {

       currentstate = RatState.Approaching; // The rat starts by approaching the cat
    }

    void Update()
    {
       switch (currentstate)
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
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized; // The direction the rat moves towards the cat
        transform.position += (Vector2)(direction * ratSpeed * Time.deltaTime); // Move the big bad rat towards the innocent cat (WHAT A MONSTER!)
    }

    void MoveAwayFromTarget()
    {
        Vector2 direction = ((Vector2)transform.position - target).normalized; // The direction the rat runs from the cat
        transform.position += (Vector2)(direction * ratRunawaySpeed * Time.deltaTime); // Move the big bad rat away from the innocent cat (YEA, AND DON'T COME BACK!)
    }

    void ReachedCat()
    {

      if (Vector2.Distance(transform.position, target) < 0.2f && !isRunningAway)
        {
            comfortLevelFunction.GetComfy(-10f); // The rat made the cat uncomfortable T-T
            isRunningAway = true;
        }
        }
}
