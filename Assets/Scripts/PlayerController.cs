using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int currentAnchor = 0;
    [SerializeField] private int selectedAchor = 0;
    [SerializeField] private int iterator = 0;
    [SerializeField] private List<int> usedIterators = new List<int>();


    [SerializeField] private GameObject yarn;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pillow;
    [SerializeField] private GameObject[] anchors = new GameObject[8];
    [SerializeField] private Animator yarnAnimator;
    [SerializeField] private TextMeshProUGUI testText;


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

        if(pillow.activeInHierarchy)
        {

            testText.text = "Kitty is Comfy";

        }
        else
        {

            testText.text = "Kitty is vulnerable";

        }



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

                        anchor.GetComponent<SpriteRenderer>().sprite = unclippedClipSprite;

                    }

                }
            }
        
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
                anchors[currentAnchor].GetComponent<SpriteRenderer>().sprite = clippedClipSprite;
                iterator++;

            }
            else
            {

                usedIterators.Remove(currentAnchor);
                anchors[currentAnchor].GetComponent<SpriteRenderer>().sprite = unclippedClipSprite;

            }


            if (usedIterators.Count >= 7)
            {

                iterator = 0;
                usedIterators.Clear();
                Debug.Log("Did a loop");


                foreach (GameObject anchor in anchors)
                {

                    anchor.GetComponent<SpriteRenderer>().sprite = unclippedClipSprite;

                }


                pillow.SetActive(true);
                currentAnchor = 0;


            }

            currentAnchor = i;
        
        }

    }

}
