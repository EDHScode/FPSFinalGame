using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;


    public override void Enter()
    {

    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;

            enemy.transform.LookAt(enemy.Player.transform);

            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            if(moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 0)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunBarrel = enemy.gunBarrel;

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f, 1f), Vector3.up) * shootDirection * 40;

        Debug.Log("Shoot");
        shotTimer = 0;
    }

    public override void Exit()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
