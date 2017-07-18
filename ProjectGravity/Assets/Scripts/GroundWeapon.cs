using UnityEngine;
using System.Collections;

public class GroundWeapon : MonoBehaviour
{

    public int _WeaponNumber;

    private bool _isPlayer = false;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _isPlayer = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.SendMessage("addGun", _WeaponNumber);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _isPlayer = false;
        }
    }

    void OnGUI()
    {
        if (_isPlayer)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 20), "Press E to Grab Weapon");
        }
    }




}
