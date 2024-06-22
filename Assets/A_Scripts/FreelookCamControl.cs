using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;

public class FreelookCamControl: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image imgCamControlArea;
    [SerializeField] CinemachineFreeLook camFreeLoock;
    string strMouseX = "Mouse X", strMouseY = "Mouse Y";

    // Start is called before the first frame update
    void Start()
    {
        imgCamControlArea = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(imgCamControlArea.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            //Debug.Log(posOut);
            camFreeLoock.m_XAxis.m_InputAxisName = strMouseX;
            camFreeLoock.m_YAxis.m_InputAxisName = strMouseY;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        camFreeLoock.m_XAxis.m_InputAxisName = null;
        camFreeLoock.m_YAxis.m_InputAxisName = null;
        camFreeLoock.m_XAxis.m_InputAxisValue = 0;
        camFreeLoock.m_YAxis.m_InputAxisValue = 0;
    }
}
