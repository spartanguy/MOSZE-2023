using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jatekosMozg : MonoBehaviour
{
        public float jatekosSeb;
        public Rigidbody2D rb;
        private Vector2 moveDirection;
        void FixedUpdate(){
            Move();
        }

        void Update(){
            ProcessInputs();
        }

        void ProcessInputs(){
            float moveX=Input.GetAxisRaw("Horizontal");
            float moveY=Input.GetAxisRaw("Vertical");

            moveDirection=new Vector2(moveX,moveY).normalized;
        }

        void Move(){
            rb.velocity = new Vector2(moveDirection.x*jatekosSeb, moveDirection.y*jatekosSeb);
        }
}
