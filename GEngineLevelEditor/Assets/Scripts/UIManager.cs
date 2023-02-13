using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ETransformType
{
    NONE,
    LOCATION,
    ROTATION,
    SCALE
}
public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject m_transformPanel = null;
    [SerializeField] private EventSystem m_eventSystem = null;
    [Space(20.0f)]
    [SerializeField] private TMP_InputField m_locationXInputField = null;
    [SerializeField] private TMP_InputField m_locationYInputField = null;
    [SerializeField] private TMP_InputField m_locationZInputField = null;
    [Space(20.0f)]
    [SerializeField] private TMP_InputField m_rotationXInputField = null;
    [SerializeField] private TMP_InputField m_rotationYInputField = null;
    [SerializeField] private TMP_InputField m_rotationZInputField = null;
    [Space(20.0f)]
    [SerializeField] private TMP_InputField m_scaleXInputField = null;
    [SerializeField] private TMP_InputField m_scaleYInputField = null;
    [SerializeField] private TMP_InputField m_scaleZInputField = null;

    //helpers
    private Transform m_currentlySelectedObject = null;
    private Vector3 m_currentTransformPosition = Vector3.zero;
    private Vector3 m_currentTransformEular = Vector3.zero;
    private Vector3 m_currentTransformScale = Vector3.zero;

    public bool GetIsSelectingUI()
    {
        return m_eventSystem.IsPointerOverGameObject();
    }
    public void UnselectedObject()
    {
        m_currentlySelectedObject = null;
        m_transformPanel.SetActive(false);
    }
    public void SelectedNewObject(Transform selectedObject)
    {
        if (!selectedObject)
            return;

        m_currentlySelectedObject = selectedObject;
        m_transformPanel.SetActive(true);

        RefreshTransformLocation();
        RefreshTransformRotation();
        RefreshTransfromScale();

    }

    public void RefreshTransform()
    {
        if (!m_currentlySelectedObject)
            return;

        if (m_currentTransformPosition != m_currentlySelectedObject.position)
            RefreshTransformLocation();

        if (m_currentTransformEular != m_currentlySelectedObject.eulerAngles)
            RefreshTransformRotation();

        if (m_currentTransformScale != m_currentlySelectedObject.localScale)
            RefreshTransfromScale();
    }

    private void RefreshTransfromScale()
    {
        Vector3 objectScale = m_currentlySelectedObject.localScale;

        m_scaleXInputField.text = objectScale.x.ToString();
        m_scaleYInputField.text = objectScale.y.ToString();
        m_scaleZInputField.text = objectScale.z.ToString();

        m_currentTransformScale = objectScale;
    }
    private void RefreshTransformRotation()
    {
        Vector3 objectEular = m_currentlySelectedObject.eulerAngles;

        m_rotationXInputField.text = objectEular.x.ToString();
        m_rotationYInputField.text = objectEular.y.ToString();
        m_rotationZInputField.text = objectEular.z.ToString();

        m_currentTransformEular = objectEular;
    }
    private void RefreshTransformLocation()
    {
        Vector3 objectPosition = m_currentlySelectedObject.position;

        m_locationXInputField.text = objectPosition.x.ToString();
        m_locationYInputField.text = objectPosition.y.ToString();
        m_locationZInputField.text = objectPosition.z.ToString();

        m_currentTransformPosition = objectPosition;
    }
    public void UpdateXLocationInputField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentPosition = m_currentlySelectedObject.position;
        currentPosition.x = numberValue;
        m_currentlySelectedObject.position = currentPosition;
    }

    public void UpdateYLocationInputField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentPosition = m_currentlySelectedObject.position;
        currentPosition.y = numberValue;
        m_currentlySelectedObject.position = currentPosition;
    }

    public void UpdateZLocationInputField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentPosition = m_currentlySelectedObject.position;
        currentPosition.z = numberValue;
        m_currentlySelectedObject.position = currentPosition;
    }

    public void UpdateXRotationInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentEular = m_currentlySelectedObject.eulerAngles;
        currentEular.x = numberValue;
        m_currentlySelectedObject.rotation = Quaternion.Euler(currentEular);
    }
    public void UpdateYRotationInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentEular = m_currentlySelectedObject.eulerAngles;
        currentEular.y = numberValue;
        m_currentlySelectedObject.rotation = Quaternion.Euler(currentEular);
    }
    public void UpdateZRotationInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentEular = m_currentlySelectedObject.eulerAngles;
        currentEular.z = numberValue;
        m_currentlySelectedObject.rotation = Quaternion.Euler(currentEular);
    }

    public void UpdateXScaleInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentScale = m_currentlySelectedObject.localScale;
        currentScale.x = numberValue;
        m_currentlySelectedObject.localScale = currentScale;
    }
    public void UpdateYScaleInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentScale = m_currentlySelectedObject.localScale;
        currentScale.x = numberValue;
        m_currentlySelectedObject.localScale = currentScale;
    }
    public void UpdateZScaleInpuField(string value)
    {
        int numberValue;
        if (!int.TryParse(value, out numberValue))
            return;

        if (!m_currentlySelectedObject)
            return;

        Vector3 currentScale = m_currentlySelectedObject.localScale;
        currentScale.x = numberValue;
        m_currentlySelectedObject.localScale = currentScale;
    }
}
