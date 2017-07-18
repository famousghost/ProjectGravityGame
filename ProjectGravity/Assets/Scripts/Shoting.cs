using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Shoting : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject _BulletHole;

    [SerializeField]
    private GameObject _BloodParticle;
[Header("Textures")]
    [SerializeField]
    private Texture2D crosshairTexture;

    [Header("GUIText")]
    [SerializeField]
    private GUIText AmmoText;

    [SerializeField]
    private GUIText RealoadText;


    [Header("AudioClip")]
    public AudioClip _PistolShots;

    public AudioClip _PistolReaload;


    private Rect posiotion;

    [Header("Value")]
    [SerializeField]
    private float _DefaultFieldOfView = 60.0f;

    [SerializeField]
    private float _Damage = 5.0f;

    [SerializeField]
    private float _ZoomFieldOfView = 40.0f;

    [SerializeField]
    private float _Range = 10f;

    public float _ShotDelay = 0.5f;

    [SerializeField]
    private float _ShotDelayCounter = 0.0f;

    [SerializeField]
    private int _CurrentAmmo = 30;

    [SerializeField]
    private int _MaxAmmo = 200;

    public GrandeThrow _GranadeThrow;

    [SerializeField]
    private int _ClipSize = 10;

    [SerializeField]
    private float _ReloadTime = 2.0f;

    [SerializeField]
    private float _Timer = 0.0f;

    [SerializeField]
    private int _CurrentClip;

    [Header("GameObjects")]
    public GameObject _PistolParticle;

    [Header("Vector3")]
    [SerializeField]
    private Vector3 _FrontOfView;

    [Header("RayCast")]
    [SerializeField]
    private RaycastHit _hit;

    [Header("BoolInfo")]
    [SerializeField]
    private bool _IsReloading = false;

    public bool _Automatic = false;

    // Use this for initialization
    void Start()
    {
        CrossHairPosiotion();
        ParticlesAndAudio();
        _CurrentClip = _ClipSize;
    }
    void ParticlesAndAudio()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _PistolParticle.GetComponent<ParticleEmitter>().emit = false;
        _PistolParticle = GameObject.FindWithTag("Emmiter");
        GetComponent<AudioSource>().GetComponent<AudioSource>().clip = _PistolShots;
    }
    void OnGUI()
    {
        GUI.DrawTexture(posiotion, crosshairTexture);
        AmmoText.pixelOffset = new Vector2(-Screen.width / 2 + 200, -Screen.height / 2 + 30);
        AmmoText.text =_CurrentClip + "/" + _CurrentAmmo + ":Ammo " + _GranadeThrow._CurrentGrandes + "/" + _GranadeThrow._MaxGrandes + " :Granades";


        if (_CurrentClip == 0)
        {
            RealoadText.enabled = true;
        }
        else
        {
            RealoadText.enabled = false;
        }
    }

    void CrossHairPosiotion()
    {
        posiotion = new Rect((Screen.width - crosshairTexture.width) / 2 + 30,
                            (Screen.height - crosshairTexture.height) / 2 + 30,
                            crosshairTexture.width / 2,
                            crosshairTexture.height / 2);
    }

    void Fire()
    {
        Transform tf = transform.parent.GetComponent<Transform>();
        _FrontOfView = tf.TransformDirection(Vector3.forward);
        if (_CurrentClip > 0 && !_IsReloading)
        {
            if ((Input.GetButtonDown("Fire1") || (Input.GetButton("Fire1") && _Automatic)) && _ShotDelayCounter<=0)
            {
                _ShotDelayCounter = _ShotDelay;
                _CurrentClip--;
                _PistolParticle.GetComponent<ParticleEmitter>().Emit();
                GetComponent<AudioSource>().GetComponent<AudioSource>().Play();
                if (Physics.Raycast(tf.position, _FrontOfView, out _hit))
                {
                    if ((_hit.transform.tag == "Enemy") && (_hit.distance < _Range))
                    {
                        _hit.transform.gameObject.SendMessage("_TakeHit", _Damage);
                        GameObject go;
                        go = Instantiate(_BloodParticle, _hit.point, Quaternion.FromToRotation(Vector3.up, _hit.normal))as GameObject;
                        Destroy(go, 0.3f);
                    }
                    else if (_hit.distance < _Range)
                    {
                        GameObject go;
                        go = Instantiate(_BulletHole, _hit.point, Quaternion.FromToRotation(Vector3.up, _hit.normal)) as GameObject;
                        Destroy(go, 5);
                        Debug.Log("Trafiona Sciana");
                    }
                }
            }
        }
    }
    public bool canGetAmmo()
    {
        if (_CurrentAmmo == _MaxAmmo)
        {
            return false;
        }
        return true;
    }
    void addAmmo(Vector2 data)
    {
        int ammoToAdd = (int)data.x;
        if (_MaxAmmo - _CurrentAmmo >= ammoToAdd)
        {
            _CurrentAmmo += ammoToAdd;
        }
        else
        {
            _CurrentAmmo = _MaxAmmo;
        }

    }
    void Reload()
    {
        if (((Input.GetButtonDown("Fire1") && _CurrentAmmo == 0) || (Input.GetButtonDown("Reload"))) && _CurrentClip < _ClipSize)
        {
            if (_CurrentAmmo > 0)
            {
                GetComponent<AudioSource>().clip = _PistolReaload;
                GetComponent<AudioSource>().Play();
                _IsReloading = true;
            }

        }
    }
    void Reloading()
    {
        if (_IsReloading)
        {
            _Timer += Time.deltaTime;
            if (_Timer > _ReloadTime)
            {
                int _needAmmo = _ClipSize - _CurrentClip;
                if (_CurrentAmmo >= _needAmmo)
                {
                    _CurrentClip = _ClipSize;
                    _CurrentAmmo -= _needAmmo;
                }
                else
                {
                    _CurrentClip += _CurrentAmmo;
                    _CurrentAmmo = 0;
                }
                GetComponent<AudioSource>().clip = _PistolShots;
                _IsReloading = false;
                _Timer = 0.0f;
            }

        }
    }
    void Zoom()
    {
        if(gameObject.GetComponentInParent<Camera>() is Camera)
        {
            Camera cam = gameObject.GetComponentInParent<Camera>();
            if(Input.GetButton("Fire2"))
            {
                if(cam.fieldOfView > _ZoomFieldOfView)
                {
                    cam.fieldOfView--;
                }
            }
            else
            {
                if (cam.fieldOfView < _DefaultFieldOfView)
                {
                    cam.fieldOfView++;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_ShotDelayCounter > 0 )
        {
            _ShotDelayCounter -= Time.deltaTime;
        }
        Zoom();
        Fire();
        Reload();
        Reloading();
    }
}
