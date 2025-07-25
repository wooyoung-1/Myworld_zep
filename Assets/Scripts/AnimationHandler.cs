using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ȯ�� ��������� x y ���� �޾� �ش簪�� ���� Ʈ���� �νĽ�Ŵ
// https://o-joyuna.tistory.com/234 ����ũ 

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
