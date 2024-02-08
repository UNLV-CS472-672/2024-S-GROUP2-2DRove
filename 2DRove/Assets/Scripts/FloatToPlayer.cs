using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloatToPlayer : MonoBehaviour
{
    private GameObject player;
    public float speed;

    void Start(){
        player = GameObject.Find("Player");
    }

    void Update(){
        if (player != null){ //this check prevents problem when player dies and there is still coin 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
        }// dealta time to make sure it's not framerate dependent
    }
}
