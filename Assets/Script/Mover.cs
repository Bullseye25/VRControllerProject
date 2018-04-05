using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour 
{
    public Controller m_controller;

    Vector3 _position;

    public float m_speed;

    // Update is called once per frame
    void Update()
    {
        if (m_controller.GetPosition() != Vector3.zero)
        {
            _position = m_controller.GetPosition();

            transform.position += _position * m_speed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
