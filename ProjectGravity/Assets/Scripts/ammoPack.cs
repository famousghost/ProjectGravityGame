using UnityEngine;
using System.Collections;

public class ammoPack : MonoBehaviour
{

    public float ammunation = 25.0f;
    public float gunType = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Transform gun = other.transform.Find("Main Camera/pistol");
            if(gun.GetComponent<Shoting>().canGetAmmo())
            {
                gun.SendMessage("addAmmo", new Vector2(ammunation, gunType));
                Destroy(gameObject);
            }
        }
            
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }
}
