using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    Player pm;
    SpriteRenderer sr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<Player>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.moveDir.x != 0 || pm.moveDir.y != 0){
            am.SetBool("Move", true);
        
            SpriteDirectionChecker();        
        }
        else{
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker(){
        if(pm.lastHorizontalVector <0){
            sr.flipX = true;
        }
        else{
            sr.flipX = false;
        }
    }
}
