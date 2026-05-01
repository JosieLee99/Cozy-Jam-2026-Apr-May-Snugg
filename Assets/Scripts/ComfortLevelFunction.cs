using UnityEngine;

public class ComfortLevelFunction : MonoBehaviour
{


    public float comfortLevel = 0f; // The current comfort level of the cat
    public float maxComfortLevel = 100f; // The maximum comfort level the cat can reach

   

    public void GetComfy(float comfortIncrease)
    {
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
        if (comfortLevel < 0f)
        {
            comfortLevel = 0f;
        }
        // Don't let the cat get too uncomfy T-T
    }

     void Start()
    {
     comfortLevel = 0f; // The starter comfort level of the cat (he's not comfy TwT)
    }

    void Update()
    {
        if (comfortLevel > 0f)
        {
            GetUncomfy(2f); // The rate at which the cat gets uncomfy (he's getting more and more uncomfy T-T)
        }
    }


}
