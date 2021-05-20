
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JugadorBola : MonoBehaviour
{
    public float force = 1.5f;
    Rigidbody rb;
    LevelManager manager;
    NavMeshAgent agent;
    public Transform WP;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
       /* if (agent && WP)
        {
            agent.SetDestination(WP.position);
        }*/
        manager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hitInfo.point, out navHit, 0.1f, NavMesh.AllAreas))
                {
                    agent.SetDestination(navHit.position);
                }
                agent.SetDestination(hitInfo.point);

            }
        }
       /* if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }*/
    }

    private void FixedUpdate()
    {
       /* if (rb)
        {
            rb.AddForce(Input.GetAxis("Horizontal") * force, 0, Input.GetAxis("Vertical") * force);
        }*/
    }
    void Jump()
    {
        if (rb && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
          GameObject.FindObjectOfType<AudioManager>().PlayJump();
            rb.AddForce(0, force *2, 0, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && manager.Score <3){
            GameObject.FindObjectOfType<AudioManager>().PlayDeath();

            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Patrol"))
        {
            GameObject.FindObjectOfType<AudioManager>().PlayDeath();

            Destroy(gameObject);
        }


    }
    private void OnCollisionStay(Collision collision)
    {

    }
    private void OnCollisionExit(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameObject.FindObjectOfType<AudioManager>().PlayCoin();

            Destroy(other.gameObject);
            manager.Score++;
        }
    }
}
