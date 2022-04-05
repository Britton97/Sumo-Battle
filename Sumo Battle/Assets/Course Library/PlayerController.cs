using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed = 5.0f;
    [SerializeField] public GameObject focalPoint;
    public bool hasPowerUp;
    public float powerUpStrength = 15.0f;
    public GameObject indicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);
        if(hasPowerUp) { indicator.transform.position = transform.position + new Vector3(0, -.5f, 0); };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            indicator.SetActive(true);
            StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 away = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(away * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(7);
        indicator.SetActive(false);
        hasPowerUp = false;
    }
}
