using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactions
{
    public class RotateObj : MonoBehaviour
    {

        RotateObj Instance;





        float rotationSpeed = 0.08f;


        private void Awake()
        {

        }

        void Start()
        {
            
        }

        
        void Update()
        {

            Rotate_(); 

        }

        public void Rotate_()
        {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Began)
                {

                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    float x = 0;
                    float y = -touch.deltaPosition.x * rotationSpeed;
                    float z = 0f;
                    transform.Rotate(x, y, z, Space.Self);
                }
                if (touch.phase == TouchPhase.Ended)
                {

                }
            }

        }
    }
}

