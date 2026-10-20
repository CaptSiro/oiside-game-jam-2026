using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Movement : PlayerBaseClass
    {
        private static readonly int WalkingUp = Animator.StringToHash("walkingUp");
        private static readonly int Speed = Animator.StringToHash("Speed");

        [Header("variables")]
        [SerializeField] private float speed;
        
        [Space(10)]
        [Header("References")]
        [SerializeField] private Animator animator;

        
        private PlayerInput input;
        private Rigidbody2D rb;
        
        private void Awake()
        {
            rb = playerRef.GetComponent<Rigidbody2D>();
            input = playerRef.GetScript<PlayerInput>();
            StartCoroutine(PlayFootstep());
        }

        public void Update()
        {
            rb.linearVelocity = input.moveInput * speed;
            if (input.moveInput == Vector2.zero)
            {
                animator.SetFloat("Speed", 0);
            }
            else
            {
                animator.SetFloat(Speed, 1);
                animator.SetBool(WalkingUp, input.moveInput.y > 0);
            }
        }

        private IEnumerator PlayFootstep()
        {
            while (gameObject.activeSelf)
            {
                if (input.moveInput != Vector2.zero)
                {
                    SoundManager.instance.PlaySound(SoundManager.Sound.footstep, transform.position);
                }

                yield return new WaitForSeconds(0.25f);
            }
        }
        
    }
}
