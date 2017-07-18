using UnityEngine;

/// <summary>
/// Source of gravity for GravityBody
/// </summary>
public class GravitySource : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private float _Force = -10;

    public Vector3 GravityUp;


    #endregion Inspector Variables

    #region Public Methods

    /// <summary>
    /// Called internally in FixedUpdate by GravityBody
    /// </summary>
    public void Affect(GravityBody gravityBody)
    {
            // Store ref to transform for ease of use
            Transform bodyTransform = gravityBody.transform;

        // Calculate vector of gravity (going 'outwards' from attractor - if body is to be pulled _Force must be negative, ie. -10)
        Vector3 gravityUp = GravityUp;

        // Store vector of body up direction (Y-axis in body local space)
        Vector3 bodyUp = gravityBody.transform.up;

        // Affect gravityBody with this attractor's force
        gravityBody.Rigidbody.AddForce(gravityUp * _Force);

        // Calculate translation of rotation: from gravityBody upwads to actual gravity vector (calculated above)
        // Translation must be multiplied by gravityBody rotation to move it from local space to world space

            Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * bodyTransform.rotation;



        // Use spherical interpolation to smoothly rotate gravityBody
        // (usefull if in future we will use more than one affector on body - that way we can smoothly rotate player between different planets)

            bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRotation, 4f * Time.deltaTime);
    }

    #endregion Public Methods
}