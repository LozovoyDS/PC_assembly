using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CPUController : MonoBehaviour
{
    [SerializeField] private bool isThermalGrease;
    [SerializeField] private bool isSocketConnection;
    private bool isRadiatorAllowed;
    public GameObject ThermalGrease; // намазанная термопаста
    public VRTK_PolicyList RadiatorZonePolicy;
    public ComponentController SocketScript;

    void Start()
    {
        SocketScript.SnapDropZone.ObjectSnappedToDropZone += OnSocketConnection; // объект установлен
        SocketScript.SnapDropZone.ObjectUnsnappedFromDropZone += OnSocketDisconnection; // объект извлечен
        // запуск проверки состояния процессора 
        StartCoroutine(CheckProcessorState()); 
    }

    void OnSocketConnection(object sender, SnapDropZoneEventArgs e)
    {
        isSocketConnection = true;
    }

    void OnSocketDisconnection(object sender, SnapDropZoneEventArgs e)
    {
        isSocketConnection = false;
    }

    private IEnumerator CheckProcessorState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (isThermalGrease && isSocketConnection && !isRadiatorAllowed)
            {
                RadiatorZonePolicy.identifiers.Add("Radiator");
                isRadiatorAllowed = true;
            }
            else if (isThermalGrease && !isSocketConnection && isRadiatorAllowed)
            {
                //var i = RadiatorZonePolicy.identifiers.IndexOf("Radiator");
                //if(i != -1) RadiatorZonePolicy.identifiers.RemoveAt(i);
                RadiatorZonePolicy.identifiers.Remove("Radiator");
                isRadiatorAllowed = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "RadiatorNotValid" && isThermalGrease && isSocketConnection) other.tag = "RadiatorValid";
        if (other.tag == "ThermalGrease")
        {
            //Destroy(other.gameObject);
            ThermalGrease.SetActive(true);
            isThermalGrease = true;
        }
    }
}