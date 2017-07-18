using UnityEngine;
using System.Collections;

public class ChangeGravity : MonoBehaviour
{
    public PlayerControler _Rigidbody;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {

                _Rigidbody._ChangeGra = !_Rigidbody._ChangeGra;
        }
    }




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
