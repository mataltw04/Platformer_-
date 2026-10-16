using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed;     //이동속도 변수 선언
    public float jumpForce;     //점프 힘 변수 선언
    public float climbSpeed;    //올라가는 속도
    public Rigidbody2D rb;      //rigidbody2d 를 rb가져오기
    public bool isAtLadder;     //사다리 붙어있는 상태
    public bool isClimbing;     //올라가는 상태
    public float jumpTimer = 1f;        //점프 쿨타임
    private float originalGravity;      //기본 중력 상태 변수 선언

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //rb 변수에 rigidbody2D 값을 저장한다.
        originalGravity = rb.gravityScale;  //기본 중력 상태 = 중력 크기 중력값 저장
    }

    private void Update()
    {
        if (Keyboard.current == null) return;   //키보드 아무것도 안누르면 복귀

        float moveX = 0f;   //moveX 변수 선언 기존 이동값 0
        float moveY = 0f;   //moveY 변수 선언 기존 이동값 0

        if (Keyboard.current.aKey.isPressed) moveX -= 1f;   //a키 누른 상태면 X축으로 -1
        if (Keyboard.current.dKey.isPressed) moveX += 1f;   //d키 누른 상태면 X축으로 +1
        if (Keyboard.current.wKey.isPressed) moveY += 1f;   //w키 누른 상태면 Y축으로 +1
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;   //s키 누른 상태면 Y축으로 -1

        if(jumpTimer > 0f)  //점프 쿨타임 0보다 크면
        {
            jumpTimer -= Time.deltaTime;    //현제 시간을 빼준다 
        }
        if(isAtLadder && Mathf.Abs(moveY) > 0f)     //만약 현제 사라리 위에 있고 , moveY의 절댓값이 0보다 크다면
        {
            isClimbing = true;      //사다리 를 타고 있는 걸로 바꿔준다.
        }

        if(isClimbing)      //현제 사다리를 타고 있다면
        {
            rb.gravityScale = 0f;   //중력을 0으로 바꿔준다.
            rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * climbSpeed);     //리지드바디에 힘을 준다.
        }
        else //사다리를 타고있지 않다면
        {
            rb.gravityScale = originalGravity;      //중력을 원래 중력으로 되돌린다.

            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);        //힘을 준다.y축은 원래 힘을 그대로 받게 놔둔다.

            if(Keyboard.current.spaceKey.wasPressedThisFrame && jumpTimer<=0f)      //스페이스바를 누르고, 잠프 타이머가 0과 같거나 더 작으면
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);     //x축에 있던 힘은 놔두고,y축에 jumpforce만큼 힘을 준다.
                jumpTimer = 1f;     //점프 타이머를 1로 다시 맞춰준다.
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)     //트리거 라는 지역에 들어오자마자
    {
        if(collision.CompareTag("Ladder"))      //닿은 물체의 태그가 Ladder라면
        {
            isAtLadder = true;      //IsAtLadder 변수를 True로 바꿔주어 올라갈 수 있게 만들어준다.
        }
    }
    private void OnTriggerExit2D(Collider2D collision)      //트리거 라는 지역에서 나가자마자
    {
        if (collision.CompareTag("Ladder"))     //닿은 물체의 태그가 Ladder라면
        {
            isAtLadder = false;     //isAtLadder 변수를 false로 바꿔준다.
            isClimbing = false;     //isClibing 변수를 false로 바꿔준다.
        }
    }

}
