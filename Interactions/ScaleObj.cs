using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PROPERTIES;


namespace Interactions
{

    public class ScaleObj : MonoBehaviour
    {

        ARTapToPlaceObject aRTapToPlaceObject;

        float initialFingersDistance;
        Vector3 initialScale;

        // Start is called before the first frame update
        void Start()
        {
            initialScale = aRTapToPlaceObject.InstantiatedScale;
        }

        // Update is called once per frame
        void Update()
        {
            Scale_();


        }

        public void Scale_()
        {
            int fingersOnScreen = 0;

            foreach (Touch touch in Input.touches)
            {
                fingersOnScreen += 1;
                if (fingersOnScreen == 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        initialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                        initialScale = transform.localScale;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        var currentFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                        var scaleFactor = currentFingersDistance / initialFingersDistance;
                        transform.localScale = initialScale * scaleFactor;
                    }
                }
            }
        }

    }

}
