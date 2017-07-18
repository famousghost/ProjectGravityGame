using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeysInput : MonoBehaviour {


    [SerializeField]
    private PlayerStats _PlayerStats;

    [Header("FloatKeys")]

    public float _WalkXKey;

    public float _WalkYKey;

    public float _RotateX;

    public float _RotateY;

    [Header("BoolInfo")]


    public bool _IsJumping = false;

    public bool _IsRunning = false;

    [Header("KeycodeKeys")]

    public KeyCode _JumpKey = KeyCode.Space;

    public KeyCode _RunKey = KeyCode.LeftShift;

    [Header("Buttons")]
    public Canvas MenuQuit;
    public Button sStart;
    public Button eExit;
    public GameObject _GameObject;

    void Awake()
    {
        _PlayerStats = _PlayerStats.GetComponent<PlayerStats>();
    }

    public void InputMenager()
    {
        _WalkXKey = Input.GetAxis("Vertical");
        _WalkYKey = Input.GetAxis("Horizontal");
        _RotateX = Input.GetAxis("Mouse X");
        _RotateY = Input.GetAxis("Mouse Y");
        _IsJumping = Input.GetKeyDown(_JumpKey);
        if (_PlayerStats.CurrentStamina > 0.0f)
        {
            _IsRunning = Input.GetKey(_RunKey);
        }
        else
        {
            _IsRunning = false;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            if (Cursor.visible)
            {
                _GameObject.active = false;
                Cursor.visible = false;
                Time.timeScale = 1; 
            }
            else
            {
                _GameObject.active = true;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }
    public void PrzyciskTakWyjdz()
    {
        if (Time.timeScale == 0)
        {
            SceneManager.LoadScene(0); // Powoduje wyjście z gry.
        }
    }
    public void PrzyciskStart()
    {
        if (Time.timeScale == 0)
        {
            _GameObject.active = false;
            Time.timeScale = 1;//włączenie czasu gry.

            Cursor.visible = false; //ukrycie kursora.
            Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora.


            sStart.enabled = true;
        }
    }
}
