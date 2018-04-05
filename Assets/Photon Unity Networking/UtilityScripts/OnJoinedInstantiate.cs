using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    //public float PositionOffset = 2.0f;
    public string m_cam, m_controller;
    public bool m_needController;
    //public GameObject[] PrefabsToInstantiate;   // set in inspector

    public void OnJoinedRoom()
    {
        if(m_needController == true)
        {
            GetThe(m_controller);
        }
        else
        {
            GetThe(m_cam);
        }

        if(m_needController == true)
        {
            m_needController = false;
        }
        else
        {
            m_needController = true;
        }
        
        //if (this.PrefabsToInstantiate != null)
        //{
        //    foreach (GameObject o in this.PrefabsToInstantiate)
        //    {
        //        Debug.Log("Instantiating: " + o.name);

        //        Vector3 spawnPos = Vector3.up;
        //        if (this.SpawnPosition != null)
        //        {
        //            spawnPos = this.SpawnPosition.position;
        //        }

        //        Vector3 random = Random.insideUnitSphere;
        //        random.y = 0;
        //        random = random.normalized;
        //        Vector3 itempos = spawnPos + this.PositionOffset * random;

        //        PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
        //    }
        //}
    }

    void GetThe(string _obj)
    {
        PhotonNetwork.Instantiate(_obj, SpawnPosition.position, Quaternion.identity, 0);
    }
}
