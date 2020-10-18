using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ComponentController : MonoBehaviour
{
    public VRTK_SnapDropZone SnapDropZone;
    public bool AddObjectTagToPolicyList = true;
    [SerializeField] private bool _componentInstalled;
    private VRTK_PolicyList _policyList;

    private void Awake()
    {
        _policyList = GetComponent<VRTK_PolicyList>();
        SnapDropZone = GetComponent<VRTK_SnapDropZone>();
    }

    private void Start()
    {
        if (AddObjectTagToPolicyList) _policyList.identifiers.Add(tag); // добовляем тег зоны установки, для того что бы в эту зону устанавливался объект с таким же тегом
        /*подписываемся на события*/
        SnapDropZone.ObjectSnappedToDropZone += OnComponentInstallation; // бъект установлен
        SnapDropZone.ObjectUnsnappedFromDropZone += OnComponentDeinstallation; // объект извлечен
    }

    private void OnComponentInstallation(object sender, SnapDropZoneEventArgs e)
    {
        _componentInstalled = true;
        
        // Отключаем возможность снять объект в режиме обучения
        if (GameManager.TrainingMode) e.snappedObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
        
        GameManager.AddProgressStep();
        Debug.Log(e.snappedObject.name + " On");
    }

    private void OnComponentDeinstallation(object sender, SnapDropZoneEventArgs e)
    {
        _componentInstalled = false;
        GameManager.RemoveProgressStep();
        Debug.Log(e.snappedObject.name + " Off");
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //Debug.Log("check  " + VRTK_PolicyList.Check(other.gameObject, _policyList));
    //     if (tag == other.tag && other.transform.localPosition == _highlightObject.localPosition)
    //     {
    //         Valid = true;
    //         GameManager.AddProgressStep();
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other) // некорректно ставит галочку в valid при отпускании объекта внутри коллайдера - баг vrtk!!!!!!!!
    // {
    //     if (tag == other.tag && other.transform.localPosition != _highlightObject.localPosition && Valid)
    //     {
    //         Valid = false;
    //         GameManager.RemoveProgressStep();
    //     }
    // }
}