using UnityEngine;
using System.Collections;

public class Grande : MonoBehaviour {

    public Vector3 Velocity;
    public float _ToExplode = 4.0f;
    public GameObject _Explosive;
    public AudioClip _ExplosionAudio;
    public AudioSource _ExplosionHolder;
    public float _Radius = 2.0f;
    public float _Force = 10.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += Velocity * Time.deltaTime;


        Explode();
	}

    void Explode()
    {
        _ToExplode -= Time.deltaTime;
        if (_ToExplode <= 0)
        {
            AudioSource _ExplosionS = Instantiate(_ExplosionHolder, transform.position, transform.rotation) as AudioSource;
            _ExplosionS.clip = _ExplosionAudio;
            _ExplosionS.Play();
            Instantiate(_Explosive, transform.position, transform.rotation);
            Destroy(gameObject);
            var colliders = Physics.OverlapSphere(transform.position, _Radius);

            foreach (var collider in colliders)
            {
                var rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(_Force, transform.position, _Radius);
                }
            }
        }
    }

}
