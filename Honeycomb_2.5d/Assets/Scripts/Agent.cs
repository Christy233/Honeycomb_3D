using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    public Animator animator;
    public HexagonManager hexagonManager;
    //public Dropdown _targeDropdown;
    //public Dropdown _startDropdown;
    public Hexagon startHex;
    public Hexagon targetHex;

    //private Hexagon startNameHex;
    //private Hexagon targetNameHex;

    private float _speed =0.5f;
    private float _time = 0.0f;
    private int i = 0;
    private List<Hexagon> route = new List<Hexagon>();
    private float _angleSpeed = 0.01f;
    private float _range = 0.5f;
    private bool isMoving = false;
    private bool isclick = false;

    public void Start()
    {

    }

    public void Update()
    {      
        if (isclick)
        {
            for(int i=0;i<route.Count;++i)
                route[i].SetRouteHex();
            Move();
            Attack();
        }
    }


    public void ClickStartButton()
    {
        //if (startNameHex.nameValue == _startDropdown.value)
        //    startHex = startNameHex;
        //if (targetNameHex.nameValue == _targeDropdown.value)
        //    targetHex = targetNameHex;

        route = hexagonManager.SearchRoute(startHex, targetHex);
        this.transform.position = startHex.transform.position;
        if (targetHex != null)
            isMoving = true;
    }

    public void ClickMoveButton()
    {
        isclick = true;
    }

    private void Move()
    {
        _time += Time.deltaTime;
        if (i < route.Count && isMoving )
        {
            //rotation
            Vector3 vec = (route[i].transform.position - transform.position);
            Quaternion rotate = Quaternion.LookRotation(vec);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, _angleSpeed);
            //move
            this.transform.position = Vector3.MoveTowards(this.transform.position, route[i].transform.position, _speed * Time.deltaTime);
            
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
