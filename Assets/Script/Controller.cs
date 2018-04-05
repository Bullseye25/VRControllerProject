using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	private float xaxis, zaxis;
	private Vector3 m_postion;
	public Transform m_cam;
    public Transform m_taller;

    internal void Start()
    {
        if (FindObjectOfType<Camera>() != null)
        {
            Camera _cam = FindObjectOfType<Camera>();

            m_cam = _cam.transform;

            //m_cam.transform.SetParent(transform);

            //StartCoroutine(FixCam());

            m_taller = m_cam.parent;
        }

        m_postion = Vector3.zero;
    }

    IEnumerator FixCam()
    {
        yield return new WaitForSeconds(0.5f);
        m_cam.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update () 
	{
        if (m_cam == null)
        {
            Start();
            return;
        }

        m_taller.position = transform.position;

		m_postion = Position ();
	}

	private Vector3 Position()
	{
		xaxis = Input.GetAxis ("Horizontal");
		zaxis = Input.GetAxis ("Vertical");

		// if player is using the controls...
		if (Input.GetAxis ("Horizontal") != 0 
			||
			Input.GetAxis ("Vertical") != 0 
			|| 
			Input.GetAxis ("Horizontal") != 0 
			&&
			Input.GetAxis ("Vertical") != 0) 
		{
            //m_cam.SetParent(null);
			Vector3 forward = m_cam.TransformDirection(Vector3.forward);

			// Player is moving on ground, Y component of camera facing is not relevant.
			forward.y = 0.0f;
			forward = forward.normalized;

			// Calculate target direction based on camera forward and direction key.
			Vector3 right = new Vector3(forward.z, 0, -forward.x);
			Vector3 targetDirection;
			targetDirection = forward * zaxis + right * xaxis;

			return targetDirection;
		} 
		else 
		{
            //m_cam.SetParent(transform);
			return Vector3.zero;
		}
	}

	public Vector3 GetPosition()
	{
		return m_postion;
	}
}