using System;
using System.Collections;
using System.Collections.Generic;
using Ash.MyUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float radius;
    public bool isSelected = false;
    public bool isBusy = false;
    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = .05f;
    private Vector3 cursorDirection;
    public float angleToCursor;
    private Vector2 mousePosition;
    private PlayerControls playerControls;
    private Camera cam;
    //public float angleToCursor;
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        cam = Camera.main;
        playerControls = new PlayerControls();
        playerControls.Gameplay.MousePosition.performed += cxt => SetMousePosition(cxt.ReadValue<Vector2>());
        playerControls.Gameplay.ActionOne.performed += cxt => ActionOne();
        playerControls.Gameplay.ActionOne.canceled += cxt => ActionOneCancelled();
        playerControls.Gameplay.ActionTwo.performed += cxt => ActionTwo();

    }

    public virtual void Update()
    {
        AdjustCursorPosition();
        angleToCursor = MyUtils.GetAngleFromVectorFloat(cursorDirection.normalized);
    }

    public void PlayerSelected()
    {
        isSelected = true;
        cursor.SetActive(true);
    }

    public void PlayerDeselected()
    {
        isSelected = false;
        rigidbody2D.velocity = Vector2.zero;
        cursor.SetActive(false);
    }

    public void PlayerCharacterMove(Vector2 dir)
    {
        if (!isBusy)
        {
            Vector3 targetVelocity = new Vector3(dir.x, dir.y) * speed;
            rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }
    }
    
    private void AdjustCursorPosition()
    {
        var originPos = transform.position;
        var mousePos = cam.ScreenToWorldPoint((mousePosition));
        var convertedMousePos = new Vector3(mousePos.x, mousePos.y, 0f);
        var aimDirection = (convertedMousePos - originPos).normalized;

        cursor.transform.position = convertedMousePos;
        cursorDirection = new Vector3(aimDirection.x, aimDirection.y, 0f);
        ;
        //Vector3 clampedPos = cursor.transform.position;
        //clampedPos.x = Mathf.Clamp(cursor.transform.position.x, originPos.x - screenBounds.x, originPos.x + screenBounds.x);
        //clampedPos.z = Mathf.Clamp(cursor.transform.position.z, originPos.z - screenBounds.y, originPos.z + screenBounds.y);
        //cursor.transform.position = clampedPos;
    }
    
    private void SetMousePosition(Vector2 pos) => mousePosition = pos;

    public virtual void ActionOne()
    {
        // perform first action
    }
    
    public virtual void ActionTwo()
    {
        // perform second action
    }

    public virtual void ActionOneCancelled()
    {
        
    }
    
    
}
