using UnityEngine;

/// <summary>
/// Allows component with rigidoby to be attracted by GravitySource
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private GravitySource[] _Source = new GravitySource[2];

    [SerializeField]
    private PlayerControler _Change;

    #endregion Inspector Variables

    #region Public Variables

    public Rigidbody Rigidbody;

    #endregion Public Variables

    #region Unity Messages

    void Start()
    {
    }

    private void FixedUpdate()
    {
        // Tell gravity source to pull this object
        if (_Change._ChangeGra == false)
        {
            _Source[0].Affect(this);
        }
        else
        {
            _Source[1].Affect(this);
            
        }

    }


    /// <summary>
    /// Called on components creation and on Reset in inspector
    /// </summary>
    private void Reset()
    {
        ValidateReferences();
    }

    /// <summary>
    /// Called each time any variable is changed in Inspector on this component
    /// </summary>
    private void OnValidate()
    {
        ValidateReferences();
    }

    #endregion Unity Messages

    #region Validation

    private void ValidateReferences()
    {
        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    }

    #endregion Validation
}