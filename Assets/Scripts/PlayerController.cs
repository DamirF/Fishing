using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;
    private int startSpeed = 50;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    public Text ScoreValue;

    private int playerScore;
    private int lineToMoveHorizontal = 1;
    protected int lineToMoveVertical = 1;
    public float lineLenHorizontal = 4; 
    public static float lineLenVertical = 15; 

    void Start()
    {
        playerScore = 0;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMoveHorizontal < 2)
                lineToMoveHorizontal++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMoveHorizontal > 0)
                lineToMoveHorizontal--;
        }

        if (SwipeController.swipeUp)
        {
            if (lineToMoveVertical < 2)
                lineToMoveVertical++;
        }

        if (SwipeController.swipeDown)
        {
            if (lineToMoveVertical > 0)
                lineToMoveVertical--;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + 1.08f * transform.up;// + transform.position.y * transform.up;
        if (lineToMoveHorizontal == 0)
            targetPosition += Vector3.left * lineLenHorizontal;
        if (lineToMoveHorizontal == 2)
            targetPosition += Vector3.right * lineLenHorizontal;
        if (lineToMoveVertical == 0)
            targetPosition += Vector3.down * lineLenVertical;
        if (lineToMoveVertical == 2)
            targetPosition += Vector3.up * lineLenVertical;

        
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

        
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.z = speed;
        controller.Move(dir * Time.fixedDeltaTime);
        Score();
    }

    void Score() {
        playerScore = (int)(transform.position.z / 2.5);
        ScoreValue.text = playerScore.ToString();
        speed = startSpeed + playerScore / 100;
    }
}