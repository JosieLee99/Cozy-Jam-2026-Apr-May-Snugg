using UnityEngine;

public class ComfortLevelFunction : MonoBehaviour
{


    public float comfortLevel = 100f; // The current comfort level of the cat
    public float maxComfortLevel = 100f; // The maximum comfort level the cat can reach

    [SerializeField] private GameObject loopGuide;

    public void GetComfy(float comfortIncrease)
    {

        if(comfortIncrease < 0)
        {

            //GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().Shake();

        }

        comfortLevel += comfortIncrease;

        if (comfortLevel > maxComfortLevel)
        {
            comfortLevel = maxComfortLevel;
        }
        // cat getting comfy UwU

    }

    public void GetUncomfy(float decreaseSpeed)
    {
        comfortLevel -= Time.deltaTime * decreaseSpeed;
        if (comfortLevel <= 0f)
        {
            comfortLevel = 0f;
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow1.SetActive(false);
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow2.SetActive(false);
            GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow3.SetActive(false);

            GameObject.FindWithTag("MainCamera").gameObject.GetComponent<Animator>().Play("Die");


        }
        // Don't let the cat get too uncomfy T-T
    }

     void Start()
    {
         comfortLevel = 100f; // The starter comfort level of the cat (he's not comfy TwT)


        GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow1.SetActive(true);
        GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow2.SetActive(true);
        GameObject.FindWithTag("MainCamera").GetComponent<PlayerController>().pillow3.SetActive(true);

    }

    void Update()
    {
        if (comfortLevel > 0f)
        {

            GetUncomfy(5f); // The rate at which the cat gets uncomfy (he's getting more and more uncomfy T-T)

        }

        if(comfortLevel < 15)
        {

            Time.timeScale = 0.25f;
            loopGuide.gameObject.SetActive(true);
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 4f;

        }
        else
        {

            Time.timeScale = 1f;
            loopGuide.gameObject.SetActive(false);
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 5f;

        }

    }


}
