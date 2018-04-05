using UnityEngine;
	
public class ProtagonistBehavior : MonoBehaviour 
{
	public static ProtagonistBehavior instance = null;

    public float m_speed;

	private Controller m_controller;

	private Rigidbody m_rigidbody;

	private Vector3 m_targetDir, m_dir;

    void Awake()
	{
		if (instance == null)
			instance = this;
	}

    void Start()
    {
        m_controller = GetComponent<Controller>();

        if (GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
            m_rigidbody = GetComponent<Rigidbody>();
        }
        else
            m_rigidbody = GetComponent<Rigidbody>();
    }

	void Update () 
	{
        Controls();     // allow to use controller	
	}

	void Controls()
	{	
		// if controller is in use.. // and not in the state of get hit
		if(Is_usingControls())
		{
			//allow protagonist to rotate
			Locomote ();

			//allow protagonist to move
			Move ();
		}
	}

	//following will be used to check either player is using movement controls or not
	private bool Is_usingControls()
	{
		return m_controller.GetPosition () != Vector3.zero;
	}

	//** following vector wil be the virtual position which will always be one step ahead of the position of the Protagonist,
	// making the movement of the character more smooth and realistic
	Vector3 VirtualPosition()
	{
		return new Vector3
			(
				//getting the current x axis of the protagonist, and adding +1 to the x axis.. returns one step a head of the current position of the protagonist
				m_rigidbody.velocity.x + m_controller.GetPosition().x
				,
				//y axis will alwasy be 0, as the only directions here that are necessary are: Forward, Backward, Left and Right
				0
				,
				//getting the current z axis of the protagonist, and adding +1 to the z axis.. returns one step a head of the current position of the protagonist
				m_rigidbody.velocity.z + m_controller.GetPosition().z
			);
	}

	//following method is being used for rotating the protagonist
	void Locomote ()
	{
		//getting the distance between the virtual position(Whic is one step a head of the current position of the protagonist) and the current position of the protagonsit
		m_targetDir = VirtualPosition () - m_rigidbody.velocity;

		//only forward, backward, left and right direction are required here
		m_dir = new Vector3 (m_targetDir.x, 0,  m_targetDir.z);

		// This will rotate the character according to the controller
		transform.rotation = Quaternion.LerpUnclamped
            (
    			transform.rotation, // get the current rotaiont of the protagonist
    			Quaternion.LookRotation (m_dir), // make protagonist look towards the direction
    			m_speed * Time.deltaTime	// giving it a speed to make a smooth turn/rotation
    		);
	}

	void Move ()
	{
		// This will move the character according to the controller
		transform.position += new Vector3 
			(
				(m_controller.GetPosition ().x * m_speed * Time.deltaTime), //updating horizontal movement
				0, 
				(m_controller.GetPosition ().z * m_speed * Time.deltaTime) //updating Vertical movement
			);
	}
}