using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShortcutsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DataManager m_dataManager = null;
    [Header("Parameters")]
    [SerializeField] private string m_ctrlDownActionName = null;
    [SerializeField] private string m_cActionName = null;
    [SerializeField] private string m_vActionName = null;
    [SerializeField] private string m_dActionName = null;
    [SerializeField] private string m_sActionName = null;
    [SerializeField] private string m_fActionName = null;

    //helpers
    private Vector3 m_currentlyCopiedObjectPosition = Vector3.zero;
    private Vector3 m_currentlyCopiedObjectRotation = Vector3.zero;
    private Vector3 m_currentlyCopiedObjectScale = Vector3.zero;
    private GameObject m_currentCopiedObject = null;
    private GameObject m_currentlySelectedObject = null;
    private GameObject m_newlyCreatedObject = null;
    private bool m_didCreateNewObject = false;
    private bool m_isPressingCtrl = false;
    private bool m_isPressingD = false;
    private bool m_isPressingC = false;
    private bool m_isPressingV = false;
    private bool m_isPressingS = false;
    private bool m_isPressingF = false;
    public void UpdateShortcutManager(PlayerInput playerInput)
    {
        UpdateInputs(playerInput);
        UpdateShortcuts();
    }

    private void UpdateShortcuts()
    {
        if (m_isPressingF)
            FocusOnObject();

        if (!m_isPressingCtrl)
            return;

        if (m_isPressingC)
            Copy();

        if (m_isPressingS)
            Save();

        if (m_isPressingD)
            Duplicate();

        if (m_isPressingV)
            Paste();
            
    }
    private void UpdateInputs(PlayerInput playerInput)
    {
        m_isPressingCtrl = playerInput.actions[m_ctrlDownActionName].IsPressed();
        m_isPressingC = playerInput.actions[m_cActionName].WasPressedThisFrame();
        m_isPressingS = playerInput.actions[m_sActionName].WasPressedThisFrame();
        m_isPressingV = playerInput.actions[m_vActionName].WasPressedThisFrame();
        m_isPressingD = playerInput.actions[m_dActionName].WasPressedThisFrame();
        m_isPressingF = playerInput.actions[m_fActionName].WasPressedThisFrame();
    }

    private void FocusOnObject()
    {
        if (!m_currentlySelectedObject)
            return;
        
        Vector3 newPos = m_currentlySelectedObject.transform.position - Vector3.forward * 10;
        newPos += Vector3.up * 10;
        Camera.main.transform.position = newPos;
        Camera.main.transform.rotation = Quaternion.LookRotation(m_currentlySelectedObject.transform.position - newPos);
        
    }
    public void UpdateSelection(Transform newObject)
    {
        if (!newObject)
            return;

        m_currentlySelectedObject = newObject.gameObject;
    }

    public void RemoveSelection()
    {
        if (!m_currentlySelectedObject)
            return;

        m_currentlySelectedObject = null;
    }
    private void Duplicate()
    {
        if (!m_currentlySelectedObject)
            return;

        m_didCreateNewObject = true;
        m_newlyCreatedObject = Instantiate(m_currentlySelectedObject, m_currentlySelectedObject.transform.parent);
    }

    private void Copy()
    {
        if (!m_currentlySelectedObject)
            return;

        m_currentCopiedObject = m_currentlySelectedObject;
        m_currentlyCopiedObjectPosition = m_currentlySelectedObject.transform.position;
        m_currentlyCopiedObjectRotation = m_currentlySelectedObject.transform.eulerAngles;
        m_currentlyCopiedObjectScale = m_currentlySelectedObject.transform.localScale;
    }

    private void Paste()
    {
        if (!m_currentCopiedObject)
            return;

        m_didCreateNewObject = true;
        m_newlyCreatedObject = Instantiate(m_currentCopiedObject,m_currentCopiedObject.transform.parent);

        m_newlyCreatedObject.transform.position = m_currentlyCopiedObjectPosition;
        m_newlyCreatedObject.transform.eulerAngles = m_currentlyCopiedObjectRotation;
        m_newlyCreatedObject.transform.localScale = m_currentlyCopiedObjectScale;
    }

    private void Save()
    {
        m_dataManager.SaveLevel(EGameType.DESCENT);
    }

    public Transform GetNewlyCreatedObject()
    {
        return m_newlyCreatedObject.transform;
    }
    public bool GetDidCreateNewObject()
    {
        bool value = m_didCreateNewObject;
        m_didCreateNewObject = false;
        return value;
    }
}
