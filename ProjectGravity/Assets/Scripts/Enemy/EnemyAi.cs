using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour
{

    [Header("OtherScripts")]
    [SerializeField]
    private PlayerStats _PlayerStats;
    [Header("Values")]
    [SerializeField]
    private float _WalkSpeed = 5f;

    [SerializeField]
    private float _AttackDistance = 3f;

    [SerializeField]
    private float _AttackDamage = 10f;

    [SerializeField]
    private float _AttackDelay = 1f;

    public float _HP = 40f;

    [SerializeField]
    private float _Timer = 0.0f;

    [Header("Transforms")]
    public Transform _Transform;

    [Header("BoolInfo")]
    public bool TakeDamage = false;

    private bool _CanTakeDamage = true;

    [Header("GuiText")]
    [SerializeField]
    private GUIText EnemyText;

    [Header("Strings")]
    [SerializeField]
    private string currentState;

    [Header("Animator")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AnimatorStateInfo stateInfo;

    void OnGUI()
    {
        EnemyText.pixelOffset = new Vector2(-Screen.width / 4 + 320, -Screen.height / 15 + 20);
        EnemyText.text = "Kill 11 enemy " + _PlayerStats._Score + "/11";
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && _HP > 0)
        {
            Quaternion _TargetRoatation = Quaternion.LookRotation(other.transform.position - transform.position);
            float _OrginalX = transform.rotation.x;
            float _OrginalZ = transform.rotation.z;
            Quaternion _FinalRoatione = Quaternion.Slerp(transform.rotation, _TargetRoatation, 5.0f * Time.deltaTime);
            _FinalRoatione.x = _OrginalX;
            _FinalRoatione.z = _OrginalZ;
            transform.rotation = _FinalRoatione;



            float _Distance = Vector3.Distance(transform.position, other.transform.position);


            if (_Distance > _AttackDistance && !stateInfo.IsName("Base Layer.wound"))
            {
                AnimationSet("run");
                transform.Translate(Vector3.forward * _WalkSpeed * Time.deltaTime);
            }
            else
            {
                if (_Timer <= 0)
                {
                    AnimationSet("attack0");
                    other.SendMessage("TakeHit", _AttackDamage);
                    TakeDamage = true;
                    _Timer = _AttackDelay;
                }
            }
            if (_Timer > 0)
            {
                _Timer -= Time.deltaTime;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            AnimationSet("idle0");
        }
    }
    void AnimationReset()
    {
        if (!stateInfo.IsName("Base Layer.idle0"))
        {
            animator.SetBool("idle0ToIdle1", false);
            animator.SetBool("idle0ToWalk", false);
            animator.SetBool("idle0ToRun", false);
            animator.SetBool("idle0ToWound", false);
            animator.SetBool("idle0ToSkill0", false);
            animator.SetBool("idle0ToAttack1", false);
            animator.SetBool("idle0ToAttack0", false);
            animator.SetBool("idle0ToDeath", false);
            animator.SetBool("runToIdle0", false);
        }
        else
        {
            animator.SetBool("idle0ToRun", false);
        }
    }
    public void AnimationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimationReset();
        if (currentState == "")
        {
            currentState = animationToPlay;
            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle0", true);
            }
            if (stateInfo.IsName("Base Layer.walk") && currentState != "walk")
            {
                animator.SetBool("walkToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.death") && currentState != "death")
            {
                animator.SetBool("deathToIdle0", true);
            }

            string state = "idle0To" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            currentState = "";

        }
    }


    void _TakeHit(float damage)
    {
        _HP -= damage;
        if (_HP <= 0 && _CanTakeDamage)
        {
            _CanTakeDamage = false;
            AnimationSet("death");
            Destroy(gameObject, 15f);
            _PlayerStats._Score += 1;
            
        }
        else if (_HP > 0)
        {
            animator.CrossFade("wound", 0.5f);
        }
    }

    // Use this for initialization
    void Start()
    {
        animator = _Transform.GetComponent<Animator>();
        currentState = "";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
