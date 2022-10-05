using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{

    private float rightBound = 15;
    public float speed = 5.0f;
    public GameObject hitEnemyParticle;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < rightBound) {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            EnemyHealth _enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            Instantiate(hitEnemyParticle, transform.position, hitEnemyParticle.transform.rotation);
            _enemyHealth.health--;
        }
    }
}
