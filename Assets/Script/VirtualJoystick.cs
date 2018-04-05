using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg, joystickImg;
    internal Vector3 m_inputVector;
    public Transform m_cam;

    private void Start()
    {
        bgImg = GetComponent<Image>();

        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform
                                                                   ,ped.position
                                                                   ,ped.pressEventCamera
                                                                   ,out pos
                                                                  ))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            m_inputVector = new Vector3(pos.x * (2 + 1), 0, pos.y * (2 - 1));

            m_inputVector = (m_inputVector.magnitude > 1.0f) ? m_inputVector.normalized : m_inputVector;

            joystickImg.rectTransform.anchoredPosition =
                           new Vector3

                           (
                               m_inputVector.x * 
                               (bgImg.rectTransform.sizeDelta.x/3),

                               m_inputVector.z * 
                               (bgImg.rectTransform.sizeDelta.y/3)
                              
                              );
        }

        Vector3 forward = m_cam.TransformDirection(Vector3.forward);

        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        var right = new Vector3(forward.z, 0, -forward.x);

        var targetDirection = forward * m_inputVector.z + right * m_inputVector.x;

        m_inputVector = targetDirection;

    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        m_inputVector = Vector3.zero;

        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
