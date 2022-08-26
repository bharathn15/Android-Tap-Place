using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;
using PROPERTIES;

namespace Interactions
{

    public class ARTapToPlaceObject : MonoBehaviour
    {
        [Header("Script")]
        [SerializeField] private EventHandler eventHandler;


        public GameObject objectToPlace;
        public GameObject placementIndicator;

        // private ARSessionOrigin arOrigin;
        private ARRaycastManager arOrigin;

        private Pose placementPose;
        private bool placementPoseIsValid = false;

        bool isPlaced = false;
        bool isInstantiated = false;

        Vector3 instantiatedPosition;
        Quaternion instantiatedRotation;
        Vector3 instantiatedScale;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        GameObject spawnedObject;
        // Camera arCamera;

        public bool IsObjectPlaced
        {
            set { isPlaced = value; }
            get { return isPlaced; }
        }

        public bool Instantiated
        {
            set { isInstantiated = value; }
            get { return isInstantiated; }
        }

        public Vector3 InstantiatedPosition
        {
            set { instantiatedPosition = value; }
            get { return instantiatedPosition; }
        }

        public Quaternion InstantiatedRotation
        {
            set { instantiatedRotation = value; }
            get { return instantiatedRotation; }
        }

        public Vector3 InstantiatedScale
        {
            set { instantiatedScale = value; }
            get { return instantiatedScale; }
        }



        /// <summary>
        /// Returns the Tap on placed Game Object
        /// </summary>
        /// <returns></returns>
        public GameObject GetSpawnedObject()
        {
            return spawnedObject;
        }

        private void Awake()
        {
            eventHandler.ToggleResetPrefabBtn(false);
        }

        void Start()
        {
            //arOrigin = FindObjectOfType<ARSessionOrigin>();
            arOrigin = FindObjectOfType<ARRaycastManager>();

            placementIndicator.transform.tag = "Placement Indicator";
            // arCamera = Properties.MainCamera.gameObject.GetComponent<Camera>();


            IsObjectPlaced = false;
        }

        void Update()
        {
            UpdatePlacementPose();
            // UpdatePlacementIndicator();


            /* Tap and Place the Object only once. */
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && IsObjectPlaced == false)
            {
                PlaceObject();

                // IsObjectPlaced = true;

                // placementIndicator.SetActive(false);
            }

        }


        /// <summary>
        /// Tap and Place the Object on the Placement Indicator.
        /// </summary>
        private void PlaceObject()
        {
            // spawnedObject = objectToPlace;
            Ray ray = Properties.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform.CompareTag(placementIndicator.transform.tag))
                {
                    if (Instantiated == false)
                    {
                        InstantiatedPosition = placementPose.position;
                        InstantiatedRotation = placementPose.rotation;

                        AddInteractionFunctionScript(objectToPlace, true);
                        AddTagToInstantiatedObject(objectToPlace);

                        spawnedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
                        InstantiatedScale = spawnedObject.transform.localScale;

                        

                        IsObjectPlaced = true;

                        Instantiated = true;
                    }
                    else if (Instantiated == true)
                    {

                        IsObjectPlaced = true;

                        ToggleSpawnedObject(true);
                    }
                }
            }



        }


        /// <summary>
        /// Once the UpdatePlacementPose() method recognises the surface, then enable the 
        /// Placement Indicator PNG image 
        /// </summary>
        private void UpdatePlacementIndicator()
        {
            if (placementPoseIsValid)
            {
              
            }
            else if (!placementPoseIsValid)
            {
                

            }
        }


        /// <summary>
        /// Check whether the Rays coming from the Camera View port is hitting the 
        /// surface, if YES then Hit Rotation will be calculated
        /// </summary>
        private void UpdatePlacementPose()
        {
            var screenCenter = Properties.MainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid && IsObjectPlaced == false)
            {
                placementPose = hits[0].pose;

                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);

                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
                
            }
            else if(placementPoseIsValid && IsObjectPlaced == true)
            {
                placementIndicator.SetActive(false);
                eventHandler.ToggleResetPrefabBtn(true);
                
            }
        }


        public void TogglePlacementIndicatorComponents(bool value)
        {

            placementIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = value;
            placementIndicator.transform.GetChild(0).GetComponent<MeshCollider>().enabled = value;

        }


        /// <summary>
        /// Add Rotate Object Script if not present
        /// </summary>
        /// <param name="Instantiating GameObject"></param>
        /// <param name="Bool"></param>
        void AddInteractionFunctionScript(GameObject obj, bool value)
        {
            var rotateObj = obj.GetComponent<RotateObj>();
            var scaleObj = obj.GetComponent<ScaleObj>();
            if (rotateObj == null)
            {
                obj.AddComponent<RotateObj>();
            }
            if (scaleObj == null)
            {
                obj.AddComponent<ScaleObj>();
            }

        }


        /// <summary>
        /// Add Tag Name to Tap and Place Game Object
        /// </summary>
        /// <param name="objectToPlace"></param>
        void AddTagToInstantiatedObject(GameObject objectToPlace)
        {
            string tagName = objectToPlace.transform.tag;
            if (tagName == null)
            {
                tagName = Properties.ObjectToPlaceTagName;
            }
        }


        /// <summary>
        /// Event Listener for Reset Button
        /// Rest for tap and place the object, once again 
        /// </summary>
        public void RePlaceObject()
        {
            Debug.Log("Reset of Prefab event is working.....");
            ToggleSpawnedObject(false);
            Instantiated = true;
            IsObjectPlaced = false;
        }

        /// <summary>
        /// Activate or Deactivate Spawned Object
        /// </summary>
        public void ToggleSpawnedObject(bool value)
        {
            try
            {
                spawnedObject.transform.position = placementPose.position;
                spawnedObject.transform.rotation = InstantiatedRotation;
                spawnedObject.transform.localScale = InstantiatedScale;
                spawnedObject.SetActive(value);
            }
            catch
            {
                Debug.LogWarning("Spawning Prefab is not accessible");
            }

        }


        private void OnDestroy()
        {

        }
    }

}