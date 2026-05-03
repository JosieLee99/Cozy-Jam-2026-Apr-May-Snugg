using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private ComfortLevelFunction comfortLevelFunction;


    [SerializeField] private int currentAnchor = 0;
    [SerializeField] private int selectedAchor = 0;
    [SerializeField] private int iterator = 0;
    [SerializeField] private List<int> usedIterators = new List<int>();


    [SerializeField] private GameObject yarn;
    [SerializeField] private GameObject canvas;
    [SerializeField] public GameObject pillow1;
    [SerializeField] public GameObject pillow2;
    [SerializeField] public GameObject pillow3;
    [SerializeField] public GameObject cat;
    [SerializeField] private GameObject[] anchors = new GameObject[8];
    [SerializeField] private Animator yarnAnimator;
    [SerializeField] public TextMeshProUGUI testText;


    [SerializeField] private Sprite unclippedClipSprite;
    [SerializeField] private Sprite clippedClipSprite;



    [SerializeField] private float firstAngle;
    [SerializeField] private float currentAngle;
    [SerializeField] private bool hasLoggedFirstAngle = false;


    void Update()
    {

        CheckForInput();
    }

    private void CheckForInput()
    {

        //if(pillow1.activeInHierarchy)
        //{
        //
        //    testText.text = "Kitty is Comfy: " + comfortLevelFunction.comfortLevel;
        //
        //}
        //else
        //{
        //
        //    testText.text = "Fin.";
        //
        //}



        Vector3 delta = transform.position - new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        angle += 180;

        for (int i = -45; i < 360; i += 45)
        {
        
            if (angle >= i && angle < i + 45)
            {

                //yarn.transform.eulerAngles = new Vector3(0f, 0f, i - 45);

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {

                    if(!hasLoggedFirstAngle)
                    {

                        firstAngle = -45 - currentAnchor * 45;

                        hasLoggedFirstAngle = true;

                    }

                    yarn.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);

                    if (i > firstAngle)
                    {

                        switch (i)
                        {
                        
                            case 0:
                        
                                    SetAnchor(1);
                        
                                break;
                            case 45:
                        
                                    SetAnchor(0);
                        
                                break;
                            case 90:
                        
                                    SetAnchor(7);
                        
                                break;
                            case 135:
                        
                                    SetAnchor(6);
                        
                                break;
                            case 180:
                        
                                    SetAnchor(5);
                        
                                break;
                            case 225:
                        
                                    SetAnchor(4);
                        
                                break;
                            case 270:
                        
                                    SetAnchor(3);
                        
                                break;
                            case 315:
                        
                                    SetAnchor(2);
                        
                                break;
                        
                        }

                    }

                }
                else
                {

                    yarn.transform.localScale = Vector3.zero;

                    iterator = 0;
                    usedIterators.Clear();
                    yarn.transform.SetParent(null);
                    hasLoggedFirstAngle = false;


                    foreach (GameObject anchor in anchors)
                    {

                        anchor.GetComponent<Animator>().SetBool("isClipped", false);

                    }

                }
            }
        
        }

        if (comfortLevelFunction.comfortLevel == 0)
        {

            pillow1.SetActive(false);
            pillow2.SetActive(false);
            pillow3.SetActive(false);

        }
        else if (comfortLevelFunction.comfortLevel <= 33)
        {

            pillow1.SetActive(true);
            pillow2.SetActive(false);
            pillow3.SetActive(false);

            if (comfortLevelFunction.comfortLevel < 20)
            {

                pillow1.GetComponent<Animator>().SetBool("isFlashing", true);

            }
            else
            {

                pillow1.GetComponent<Animator>().SetBool("isFlashing", false);

            }

        }
        else if (comfortLevelFunction.comfortLevel > 33 && comfortLevelFunction.comfortLevel <= 66)
        {

            pillow1.SetActive(true);
            pillow2.SetActive(true);
            pillow3.SetActive(false);

            if (comfortLevelFunction.comfortLevel < 50)
            {

                pillow2.GetComponent<Animator>().SetBool("isFlashing", true);

            }
            else
            {

                pillow2.GetComponent<Animator>().SetBool("isFlashing", false);

            }

        }
        else if (comfortLevelFunction.comfortLevel > 66)
        {

            pillow1.SetActive(true);
            pillow2.SetActive(true);
            pillow3.SetActive(true);

            if (comfortLevelFunction.comfortLevel < 80)
            {

                pillow3.GetComponent<Animator>().SetBool("isFlashing", true);

            }
            else
            {

                pillow3.GetComponent<Animator>().SetBool("isFlashing", false);

            }

            currentAnchor = 0;


        }

    }

    private void SetAnchor(int i)
    {

        if (currentAnchor != i)
        {

            yarnAnimator.SetInteger("currentAnchor", i);


            if (!usedIterators.Contains(currentAnchor))
            {

                usedIterators.Add(currentAnchor);
                anchors[currentAnchor].GetComponent<Animator>().SetBool("isClipped", true);
                iterator++;

            }
            else if(usedIterators.Contains(currentAnchor))
            {

                //usedIterators.Remove(currentAnchor);
                anchors[currentAnchor].GetComponent<Animator>().SetBool("isClipped", false);

            }


            if (usedIterators.Count >= 6 && comfortLevelFunction.comfortLevel > 0)
            {

                iterator = 0;
                usedIterators.Clear();
                Debug.Log("Did a loop");


                foreach (GameObject anchor in anchors)
                {

                    anchor.GetComponent<Animator>().SetBool("isClipped", false);

                }

                comfortLevelFunction.GetComfy(33f);


                //if (comfortLevelFunction.comfortLevel == 0)
                //{
                //
                //    pillow1.SetActive(false);
                //    pillow2.SetActive(false);
                //    pillow3.SetActive(false);
                //
                //}
                //else if (comfortLevelFunction.comfortLevel <= 33)
                //{
                //
                //    pillow1.SetActive(true);
                //    pillow2.SetActive(false);
                //    pillow3.SetActive(false);
                //
                //    if (comfortLevelFunction.comfortLevel < 20)
                //    {
                //
                //        pillow1.GetComponent<Animator>().SetBool("isFlashing", true);
                //
                //    }
                //    else
                //    {
                //
                //        pillow1.GetComponent<Animator>().SetBool("isFlashing", false);
                //
                //    }
                //
                //}
                //else if (comfortLevelFunction.comfortLevel > 33 && comfortLevelFunction.comfortLevel <= 66)
                //{
                //
                //    pillow1.SetActive(true);
                //    pillow2.SetActive(true);
                //    pillow3.SetActive(false);
                //
                //    if (comfortLevelFunction.comfortLevel < 50)
                //    {
                //
                //        pillow2.GetComponent<Animator>().SetBool("isFlashing", true);
                //
                //    }
                //    else
                //    {
                //
                //        pillow2.GetComponent<Animator>().SetBool("isFlashing", false);
                //
                //    }
                //
                //}
                //else if (comfortLevelFunction.comfortLevel > 66)
                //{
                //
                //    pillow1.SetActive(true);
                //    pillow2.SetActive(true);
                //    pillow3.SetActive(true);
                //
                //   if (comfortLevelFunction.comfortLevel < 80)
                //   {
                //   
                //       pillow3.GetComponent<Animator>().SetBool("isFlashing", true);
                //   
                //   }
                //   else
                //   {
                //   
                //       pillow3.GetComponent<Animator>().SetBool("isFlashing", false);
                //   
                //   }
                //
                //    currentAnchor = 0;
                //
                //
                //}

            }

            currentAnchor = i;

        }

    }

    //public void Shake()
    //{
//
    //    StartCoroutine(ShakeObjectOnce());
//
    //}

    //public IEnumerator ShakeObjectOnce()
    //{
    //
    //    int j = 0;
    //
    //    Vector3 startingPosition = cat.transform.localPosition;
    //
    //    float x = UnityEngine.Random.Range(0f, 0.1f);
    //    float y = UnityEngine.Random.Range(0f, 0.1f);
    //
    //    gameObject.transform.localPosition = new Vector3(startingPosition.x + x, startingPosition.y + y, gameObject.transform.localPosition.z);
    //
    //    j++;
    //
    //    yield return new WaitForSeconds(0.1f);
    //
    //    gameObject.transform.position = Vector3.zero;
    //
    //}

}
