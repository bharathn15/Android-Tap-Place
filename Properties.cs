using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PROPERTIES
{
    public class Properties
    {

        private static Camera mainCamera = Camera.main;

        readonly private static string objectToPlaceTagName = "Spawnable Object";

        public static Camera MainCamera
        {
            get { return mainCamera; }
        }

        public static string ObjectToPlaceTagName
        {
            get { return objectToPlaceTagName; }
        }



    }
}


