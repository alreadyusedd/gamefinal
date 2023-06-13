using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


  public CharacterController2D controller;

  public float runSpeed = 40f;
  float horizontalMove = 0f;
  bool jump = false;
  bool crouch = false;

  [SerializeField] private FixedJoystick _joystick;
  [SerializeField] private Rigidbody2D rb;
  private Vector2 _movement;

  [SerializeField] private LayerMask _groundLayer;
  [SerializeField] private Transform _groundCheck;
  [SerializeField] private GameObject inventoryGO;
  public float jump1 = 5f;

  private void Start()
  {
    inventoryGO.SetActive(!inventoryGO.activeSelf);
  }

  void Update()
  {

    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;



    if (Input.GetButtonDown("Jump"))
    {
      jump = true;
    }

    if (Input.GetButtonDown("Crouch"))
    {
      crouch = true;
    }
    else if (Input.GetButtonUp("Crouch"))
    {
      crouch = false;
    }

    if (Input.GetKeyDown(KeyCode.I))
    {
      inventoryGO.SetActive(!inventoryGO.activeSelf);
    }

    if (_joystick.Horizontal >= .2f)
    {
      horizontalMove = runSpeed;
    }
    else if (_joystick.Horizontal <= -.2f)
    {
      horizontalMove = -runSpeed;
    }
  }

  void FixedUpdate()
  {
    controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    jump = false;
  }

  public void Jump()
  {
    if (IsGrounded())
    {
      rb.velocity = new Vector2(rb.velocity.x, jump1);
    }

  }



  private bool IsGrounded()
  {
    return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
  }
}