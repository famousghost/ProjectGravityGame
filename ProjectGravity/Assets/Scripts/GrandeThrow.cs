using UnityEngine;
using System.Collections;

public class GrandeThrow : MonoBehaviour
{

    public Rigidbody _Grande;

    public int _CurrentGrandes = 2;

    public int _MaxGrandes = 4;

    public GUIText _Granades;

    void OnGUI()
    {
      /*  _Granades.pixelOffset = new Vector2(Screen.width / 15 - 100, -Screen.height / 15 + 39);
        _Granades.text = _CurrentGrandes + "/" + _MaxGrandes;*/
    }

    void ThrowGranade()
    {
        if (Input.GetKeyDown(KeyCode.F) && _CurrentGrandes > 0)
        {
            Rigidbody _Clone = Instantiate(_Grande, transform.position, transform.rotation) as Rigidbody;
            _Clone.AddForce(transform.TransformDirection(Vector3.forward * 1000));



            _CurrentGrandes--;
        }
    }
    void AddGrande(Vector2 data)
    {
        int _AddGranade = (int)data.x;
        if (_MaxGrandes - _CurrentGrandes >= _AddGranade)
        {
            _CurrentGrandes += _AddGranade;
        }
        else
        {
            _CurrentGrandes = _MaxGrandes;
        }
    }
    public bool canGetAmmo()
    {
        if (_CurrentGrandes == _MaxGrandes)
        {
            return false;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        ThrowGranade();
    }
}
