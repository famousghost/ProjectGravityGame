using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrigerOfDeath : MonoBehaviour {

    public PlayerStats _PlayerStats;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            SceneManager.LoadScene(2);
        }
            Destroy(other.gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        if(other.tag.Equals("Enemy"))
        {
            _PlayerStats._Score += 1;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
