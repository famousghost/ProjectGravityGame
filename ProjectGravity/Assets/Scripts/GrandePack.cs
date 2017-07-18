using UnityEngine;
using System.Collections;

public class GrandePack : MonoBehaviour
{

    private float _GranadeGet = 1.0f;

    private float _GranadeType = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        Transform _Granade = other.transform.Find("Main Camera");
        if(other.tag.Equals("Player"))
        {
            if(_Granade.GetComponent<GrandeThrow>().canGetAmmo())
            {
                _Granade.SendMessage("AddGrande", new Vector2(_GranadeGet,_GranadeType));
                Destroy(gameObject);
            }            
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }
}
