using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterLocomotion : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public Camera cam;
    public GameObject pivot;
    public GameObject head;

    Animator anim;

    bool stun = false;
    bool dead = false;
    bool encounter = false;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        anim.SetFloat("animSpeed", 1);
    }

    void Update()
    {
        if(timer < 0 && encounter)
        {
            anim.SetFloat("animSpeed", 1);
            stun = false;
            int randValueX = Random.Range(-20, 20);
            int randValueZ = Random.Range(-20, 20);

            anim.SetBool("isWalk", true);
            anim.SetBool("isRun", false);
            agent.speed = 2;
            timer = 10f;
            agent.SetDestination(new Vector3(target.transform.position.x + randValueX,
                target.transform.position.y, target.transform.position.z + randValueZ));
        }
        else
            timer -= Time.deltaTime;

        if (dead)
        {
            cam.transform.position = pivot.transform.position;
            cam.transform.LookAt(head.transform.position);
            anim.SetBool("isRun", false);
            anim.SetFloat("animSpeed", 0);
        }
    }

    public void Stun()
    {
        if(!stun)
            StartCoroutine("SlowDown");
        stun = true;
        timer = 2f;
        agent.SetDestination(transform.position);    
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player" && !stun)
        {
            encounter = true;
            anim.speed = 1f;
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", true);
            agent.speed = 6;
            timer = 1f;
            agent.SetDestination(target.position);

            //충돌 애니매이션 출력
            if((target.position - transform.position).magnitude < 2 && !dead)
            {
                StartCoroutine("DeadScene");
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player" && !stun)
        {
            anim.SetBool("isRun", false);
            agent.speed = 2;
            agent.SetDestination(transform.position);
        }
    }

    IEnumerator SlowDown()
    {
        for (int i = 0; i < 10; i++)
        {
            anim.SetFloat("animSpeed", 1 - i / 10f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetFloat("animSpeed", 0);
    }

    IEnumerator DeadScene()
    {
        target.GetComponent<PlayerControl>().enabled = false;
        cam.GetComponent<LightControl>().lightCom.intensity = 0;
        cam.GetComponent<LightControl>().enabled = false;
        cam.transform.parent = null;
        dead = true;
        
        anim.SetTrigger("collision");
        for (int i = 0; i < 40; i++)
        {
            float randValueX = Random.Range(-2, 2) / 10000f;
            float randValueY = Random.Range(-2, 2) / 10000f;
            pivot.transform.localPosition = new Vector3(randValueX, randValueY, 0.004f);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 10; i++)
        {
            float randValue = Random.Range(-2, 2) / 10000f;
            pivot.transform.localPosition = new Vector3(randValue, i / 100000f + randValue, 0.004f - i / 2000f);
            yield return new WaitForSeconds(0.01f);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
