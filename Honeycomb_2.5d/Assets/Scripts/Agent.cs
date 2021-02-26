using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Animator animator;
    public HexagonManager hexagonManager;
    public Hexagon startHex;//implement： ues OnTriggerEnter()? automatically find startHex which is agent stand on.
    public Hexagon targetHex;//The enemy stand on

    private float _speed =0.5f;
    private float _time = 0.0f;
    private int i = 0;
    private List<Hexagon> route = new List<Hexagon>();
    private float _angleSpeed = 0.01f;
    private float _range = 0.5f;
    private bool isMoving = false;

    public void Start()
    {
        route = hexagonManager.SearchRoute(startHex, targetHex);
        this.transform.position = startHex.transform.position;
        if(targetHex != null)
            isMoving = true;
    }

    public void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        _time += Time.deltaTime;        
        if(i < route.Count && isMoving )
        {
            //rotation
            Vector3 vec = (route[i].transform.position - transform.position);
            Quaternion rotate = Quaternion.LookRotation(vec);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, _angleSpeed);
            //move
            this.transform.position = Vector3.MoveTowards(this.transform.position, route[i].transform.position, _speed * Time.deltaTime);
            route[i].SetRouteHex();
            if (i == 0 || i==route.Count-1)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isIdle", true);
            }
            else
            {
                animator.SetBool("isMoving", true);
                animator.SetBool("isIdle", false);
            }
            if (_time > 2.0f)
            {
                i++;
                _time = 0.0f;
            }
        }
    }

    private void Attack()
    {
        float dist = Vector3.Distance(this.transform.position, targetHex.transform.position);
        if(dist<_range)
        {
            isMoving = false;
            animator.SetBool("isAttack", true);
            animator.SetBool("Death", false);
            animator.SetBool("isMoving", false);
            animator.SetBool("isIdle", false);
        }
    }
}
