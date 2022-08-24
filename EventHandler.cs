using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactions;


public class EventHandler : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private ARTapToPlaceObject aRTapToPlaceObject;

    [SerializeField] private GameObject resetPrefabBtn;


    public GameObject ResetPrefabBtn
    {
        set { resetPrefabBtn = value; }
        get { return resetPrefabBtn; }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// <summary>
    /// Reset/Replace Prefab.
    /// </summary>

    public virtual void ResetPrefab()
    {
        Debug.Log("Rest Prefab Method is working......");
        GameObject spawnedPrefab = aRTapToPlaceObject.GetSpawnedObject();
        
        if (aRTapToPlaceObject.Instantiated && aRTapToPlaceObject.GetIsObjectPlaced() == true)
        {
            Debug.Log(aRTapToPlaceObject.GetIsObjectPlaced());
            
            spawnedPrefab.SetActive(false);
            aRTapToPlaceObject.SetIsObjectPlaced(false);

            

        }
        else if(aRTapToPlaceObject.Instantiated && aRTapToPlaceObject.GetIsObjectPlaced() == false)
        {
            Debug.Log(aRTapToPlaceObject.GetIsObjectPlaced());
            
            spawnedPrefab.SetActive(true);
            aRTapToPlaceObject.SetIsObjectPlaced(true);

            
        }

    }


    /// <summary>
    /// Activate/Deactivate the Reset Prefab Button
    /// </summary>
    /// <param name="value"></param>
    public void ToggleResetPrefabBtn(bool value)
    {
        ResetPrefabBtn.SetActive(value);
    }






}
