using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PROPERTIES
{
    public class Properties
    {

        private static Camera mainCamera = Camera.main;

        readonly private static string objectToPlaceTagName = "Spawnable Object";

        readonly private static float rotationSpeed = 0.1f;

        public static Camera MainCamera
        {
            get { return mainCamera; }
        }

        public static string ObjectToPlaceTagName
        {
            get { return objectToPlaceTagName; }
        }

        public static float RotationSpeed
        {
            get { return rotationSpeed; }
        }



    }
}


