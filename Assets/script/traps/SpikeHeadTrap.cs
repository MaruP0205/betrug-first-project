using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadTrap : enemyDamge
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDeplay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;



    private void OnEnable(){
        Stop();
    }

    private void Update(){
       if(attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDeplay)
                CheckForPlayer();
        }
    }
    
    private void CheckForPlayer(){
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking){
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections(){
        directions[0] = transform.right * range; //Right Direction
        directions[1] = -transform.right * range; //Left Direction
        directions[2] = transform.up * range; //Up Direction
        directions[3] = -transform.up * range; //Down Direction
    }

    private void Stop(){
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        base.OnTriggerEnter2D(other);
        Stop();
    }
}
