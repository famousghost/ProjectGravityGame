using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("OtherScripts")]
    [SerializeField]
    private KeysInput _KeysInput;

    [SerializeField]
    private PlayerControler _PlayerControler;

    [Header("Values")]
    [SerializeField]
    private float MaxHealth = 100f;

    [SerializeField]
    private float CurrentHealth = 100f;

    [SerializeField]
    private float MaxArmour = 100f;

    [SerializeField]
    private float CurrentArmour = 100f;

    [SerializeField]
    private float MaxStamina = 100f;

    [SerializeField]
    public float CurrentStamina = 100f;

    [SerializeField]
    public float CanHeal = 0.0f;

    [SerializeField]
    public int _Score = 0;

    [SerializeField]
    public float CanRegenerate = 0.0f;

    [SerializeField]
    public float Opacity = 0.0f;


    [Header("ResponsiveToScreen")]

    private float barWidth;
    private float barHeight;

    [Header("boolInfo")]
    [SerializeField]
    private bool hit = false;

    private bool _damaged = false;

    [Header("Textured")]
    public Texture2D HealthTexture;
    public Texture2D ArmourTexture;
    public Texture2D StaminaTexture;
    public Texture2D BloodTexture;

    void Awake()
    {
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;
    }

    void OnGUI()
    {
        Hiting();

        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight - 10,
                                 CurrentHealth * barWidth / MaxHealth,
                                 barHeight),
                                 HealthTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                         Screen.height - barHeight * 2 - 20,
                         CurrentArmour * barWidth / MaxArmour,
                         barHeight),
                         ArmourTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                         Screen.height - barHeight * 3 - 30,
                         CurrentStamina * barWidth / MaxStamina,
                         barHeight),
                         StaminaTexture);
    }

    void TakeHit(float damage)
    {
        _damaged = true;
        if(CurrentArmour > 0)
        {
            CurrentArmour -= damage;
            {
                if(CurrentArmour < 0)
                {
                    CurrentHealth += CurrentArmour;
                    CurrentArmour = 0;
                }
            }
        }
        else
        {
            CurrentHealth -=damage;
        }

        if(CurrentHealth < MaxHealth)
        {
            CanHeal = 3.0f;
        }

        CurrentArmour = Mathf.Clamp(CurrentArmour, 0, MaxArmour);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

    }

    void Stamina()
    {
        if(_KeysInput._IsRunning && CurrentStamina > 0 &&  _PlayerControler.LastPosition != transform.position && _PlayerControler._IsGrounded)
        {
            _PlayerControler.LastPosition = transform.position;
            CurrentStamina -= 1f;
            CurrentStamina = Mathf.Clamp(CurrentStamina, 0, MaxStamina);
            CanRegenerate = 3.0f;
        }
    }

    void regen(ref float CurrentStat, float MaxStat)
    {
        CurrentStat += MaxStat * 0.1f;
        Mathf.Clamp(CurrentStat, 0, MaxStat);
    }

    void Regenrate()
    {
        if (CanHeal > 0.0f)
        {
            CanHeal -= Time.deltaTime;
        }
        if (CanRegenerate > 0.0f)
        {
            CanRegenerate -= Time.deltaTime;
        }

        if (CanHeal <= 0.0f && CurrentHealth < MaxHealth)
        {
            regen(ref CurrentHealth, MaxHealth);
        }
        if (CanRegenerate <= 0.0f && CurrentStamina < MaxStamina)
        {
            regen(ref CurrentStamina, MaxStamina);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
        Regenrate();
        Stamina();
        if(_Score == 11)
        {
            SceneManager.LoadScene(3);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        if(CurrentHealth <= 0)
        {
            SceneManager.LoadScene(2);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }

    }
    void Hiting()
    {
        if (_damaged == true)
        {
            _damaged = false;
            hit = true;
            Opacity = 1.0f;
        }
        
        if(hit)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Opacity);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BloodTexture, ScaleMode.ScaleToFit);
            StartCoroutine("WaitAndChangeOpacity");
        }
        if(Opacity< 0.0f)
        {
            hit = false;
        }

    }
    IEnumerator WaitAndChangeOpacity()
    {
        yield return new WaitForEndOfFrame();
        Opacity -= 0.05f;
    }
}
