using UnityEngine;
using System.Collections;

public class WeaponInventory : MonoBehaviour
{

    public GameObject[] _GunList = new GameObject[10];
    [SerializeField]

    private bool[] _guns = new bool[] { false, true, false, false, false, false, false, false, false, false };
    private KeyCode[] _Keys = new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    private int _maxGuns = 1;

    public void addGun(int number)
    {
        _guns[number] = true;
        _maxGuns++;
    }


    void Update()
    {
        for (int i = 0; i < _Keys.Length; i++)
        {
            if (Input.GetKeyDown(_Keys[i]) && _guns[i])
            {
                hideGuns();
                _GunList[i].SetActive(true);
            }
        }
    }
    private void hideGuns()
    {
        for (int i = 1; i < _maxGuns + 1; i++)
        {
            _GunList[i].SetActive(false);
        }
    }
}
