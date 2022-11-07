using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum MoveState { Left, Right, Up, None};
public class PlayerBehaviour : MonoBehaviour
{
    float width = Screen.width;
    Touch touch;
    public float moveSpeed = 3.0f;
    public float turnSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public LayerMask groundLayer;
    public GameObject _road;
    private Rigidbody _rb;
    private BoxCollider _col;
    public int linesToMove = 3;
    public static float lineSize;
    private float roadBoundWidth;
    private float roadScaleX;
    public static float roadRealWidth;
    float swipeStartPosX, swipeStartPosY, swipeEndPosX, swipeEndPosY;


    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        roadBoundWidth = _road.GetComponent<MeshFilter>().mesh.bounds.size.x;
        roadScaleX = _road.GetComponent<Transform>().localScale.x;
        roadRealWidth = roadBoundWidth * roadScaleX;
        lineSize = Mathf.Floor(roadRealWidth / linesToMove);
        Debug.LogFormat("Each size of lines to move equal {0}", lineSize);
        
        Debug.Log(width);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            swipeControl(touch);
        }

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    void swipeControl(Touch touch)
    {
        
        switch (touch.phase)
        {
            case TouchPhase.Began:

                swipeStartPosX = touch.position.x;
                swipeStartPosY = touch.position.y;
                break;
            case TouchPhase.Ended:
                swipeEndPosX = touch.position.x;
                swipeEndPosY = touch.position.y;
                Vector2 currentSwipe = new Vector2(swipeEndPosX - swipeStartPosX, swipeEndPosY - swipeStartPosY);
                currentSwipe.Normalize();
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // left
                {
                    Debug.Log("Left swipe");
                   
                    this.transform.Translate(Vector3.left * lineSize);
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) // right
                {
                    Debug.Log("Right swipe");
                    
                    this.transform.Translate(Vector3.right * lineSize);
                }
                else if (IsGrounded() && currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // up
                {
                    Debug.Log("Up swipe");
                 
                    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    
                }
                break;
        }
    }

    void moveControl(MoveState state)
    {
        switch (state)
        {
            case MoveState.Left:
                _rb.MovePosition(transform.position - new Vector3(100f, 0, 0) * Time.fixedDeltaTime);
                break;
            case MoveState.Right:
                _rb.MovePosition(transform.position + new Vector3(100f, 0, 0) * Time.fixedDeltaTime);
                break;
            case MoveState.Up:
                _rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
                break;
        }
    }

    bool IsGrounded()
    {
        Vector3 halfSize = new Vector3(_col.bounds.size.x / 2, _col.bounds.size.y / 2, _col.bounds.size.z / 2);
        bool grounded = Physics.CheckBox(_col.bounds.center, halfSize, _col.transform.rotation, groundLayer, QueryTriggerInteraction.Ignore); ;
        Debug.Log(grounded);
        return grounded;
    }
 
}
