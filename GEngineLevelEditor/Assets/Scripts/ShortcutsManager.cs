using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShortcutsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DataManager m_dataManager = null;
    
    [Header("Parameters")]
    [SerializeField] private string m_deleteActionName = null;
    [SerializeField] private string m_ctrlDownActionName = null;
    [SerializeField] private string m_shiftActionName = null;
    [SerializeField] private string m_cActionName = null;
    [SerializeField] private string m_vActionName = null;
    [SerializeField] private string m_dActionName = null;
    [SerializeField] private string m_sActionName = null;
    [SerializeField] private string m_fActionName = null;
    [SerializeField] private string m_lActionName = null;

    //helpers
    private List<Vector3> m_currentlyCopiedObjectPosition = new List<Vector3>();
    private List<Vector3> m_currentlyCopiedObjectRotation = new List<Vector3>();
    private List<Vector3> m_currentlyCopiedObjectScale = new List<Vector3>();
    private List<Transform> m_currentCopiedObject = new List<Transform>();
    private List<Transform> m_currentlySelectedObject = new List<Transform>();
    private List<Transform> m_newlyCreatedObject = new List<Transform>();
    private bool m_didCreateNewObject = false;
    private bool m_didDeleteSelectedObject = false;
    private bool m_isPressingShift = false;
    private bool m_isPressingCtrl = false;
    private bool m_isPressingD = false;
    private bool m_isPressingC = false;
    private bool m_isPressingV = false;
    private bool m_isPressingS = false;
    private bool m_isPressingF = false;
    private bool m_isPressingL = false;
    private bool m_isPressingDelete = false;

    public bool GetIsHoldingShift() { return m_isPressingShift; }
    public void UpdateShortcutManager(PlayerInput playerInput)
    {
        UpdateInputs(playerInput);
        UpdateShortcuts();
    }

    private void UpdateShortcuts()
    {
        if (m_isPressingF)
            FocusOnObject();

        if (m_isPressingDelete)
            DeleteSelectedObject();

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

        if (m_isPressingL)
            LoadLevel();
            
    }
    private void UpdateInputs(PlayerInput playerInput)
    {
        m_isPressingShift = playerInput.actions[m_shiftActionName].IsPressed();
        m_isPressingCtrl = playerInput.actions[m_ctrlDownActionName].IsPressed();
        m_isPressingC = playerInput.actions[m_cActionName].WasPressedThisFrame();
        m_isPressingS = playerInput.actions[m_sActionName].WasPressedThisFrame();
        m_isPressingV = playerInput.actions[m_vActionName].WasPressedThisFrame();
        m_isPressingD = playerInput.actions[m_dActionName].WasPressedThisFrame();
        m_isPressingF = playerInput.actions[m_fActionName].WasPressedThisFrame();
        m_isPressingL = playerInput.actions[m_lActionName].WasPressedThisFrame();
        m_isPressingDelete = playerInput.actions[m_deleteActionName].WasPressedThisFrame();
    }

    private void DeleteSelectedObject()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;

        for (int i = 0; i < m_currentlySelectedObject.Count; i++)
        {
            Destroy(m_currentlySelectedObject[i].gameObject);
        }
        m_didDeleteSelectedObject = true;
    }
    private void FocusOnObject()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;
        
        Vector3 newPos = m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].position - Vector3.forward * 10;
        newPos += Vector3.up * 10;
        Camera.main.transform.position = newPos;
        Camera.main.transform.rotation = Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].position - newPos);
        
    }
    public void UpdateSelection(List<Transform> newObject)
    {
        if (newObject.Count == 0)
            return;

        m_currentlySelectedObject = newObject;
    }

    public void RemoveSelection()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;

        m_currentlySelectedObject.Clear();
    }
    private void Duplicate()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;

        List<Transform> newlyCreatedObjects = new List<Transform>();
        foreach (Transform selectedObject in m_currentlySelectedObject)
        {
            newlyCreatedObjects.Add(Instantiate(selectedObject.gameObject, selectedObject.parent).transform);
        }
        m_didCreateNewObject = true;
        m_newlyCreatedObject = newlyCreatedObjects;
    }

    private void Copy()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;

        m_currentlyCopiedObjectPosition.Clear();
        m_currentlyCopiedObjectRotation.Clear();
        m_currentlyCopiedObjectScale.Clear();
        m_currentCopiedObject = m_currentlySelectedObject;
        for (int i = 0; i < m_currentlySelectedObject.Count; i++)
        {
            m_currentlyCopiedObjectPosition.Add(m_currentlySelectedObject[i].position);
            m_currentlyCopiedObjectRotation.Add(m_currentlySelectedObject[i].eulerAngles);
            m_currentlyCopiedObjectScale.Add(m_currentlySelectedObject[i].localScale);
        }
    }

    private void Paste()
    {
        if (m_currentCopiedObject.Count == 0)
            return;

        m_didCreateNewObject = true;
        List<Transform> newlyCreatedObjects = new List<Transform>();
        GameObject createdObject = null;
        for (int i = 0; i < m_currentlySelectedObject.Count; i++)
        {
            createdObject = Instantiate(m_currentlySelectedObject[i].gameObject, m_currentlySelectedObject[i].parent);
            createdObject.transform.position = m_currentlyCopiedObjectPosition[i];
            createdObject.transform.eulerAngles = m_currentlyCopiedObjectRotation[i];
            createdObject.transform.localScale = m_currentlyCopiedObjectScale[i];

            newlyCreatedObjects.Add(createdObject.transform);
        }
        
        m_didCreateNewObject = true;
        m_newlyCreatedObject = newlyCreatedObjects;
    }

    private void Save()
    {
        m_dataManager.SaveLevel(EGameType.DESCENT);
    }

    private void LoadLevel()
    {
        m_dataManager.LoadLevel(EGameType.DESCENT);
    }
    
    public List<Transform> GetNewlyCreatedObject()
    {
        return m_newlyCreatedObject;
    }
    public bool GetDidCreateNewObject()
    {
        bool value = m_didCreateNewObject;
        m_didCreateNewObject = false;
        return value;
    }

    public bool GetDidDeleteSelectedObject()
    {
        bool value = m_didDeleteSelectedObject;
        m_didDeleteSelectedObject = false;
        return value;
    }
}
