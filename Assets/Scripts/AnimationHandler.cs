using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 방향 전환을 만들기위해 x y 값을 받아 해당값을 블렌더 트리로 인식시킴
// https://o-joyuna.tistory.com/234 참고링크 

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int XDir = Animator.StringToHash("xDir");
    private static readonly int YDir = Animator.StringToHash("yDir");

    private Vector2 lastDirection = Vector2.down;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.01f;
        animator.SetBool(IsMoving, obj.magnitude > .5f);

        if (isMoving)
        {
            lastDirection = obj.normalized;
        }

        animator.SetFloat(XDir, lastDirection.x);
        animator.SetFloat(YDir, lastDirection.y);
    }

}
