using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BirdController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;
    public float moveForce = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().useGravity = true;
    }


    // This method MUST match the Input Action name exactly: MoveVertical
    public void OnMoveVertical(InputValue value)
    {
        moveInput.y = value.Get<float>();
    ///    Debug.Log("Vertical input: " + moveInput.y);
    }

    // This method MUST match the Input Action name exactly: MoveHorizontal
    public void OnMoveHorizontal(InputValue value)
    {
        moveInput.x = value.Get<float>();
    ///    Debug.Log("Horizontal input: " + moveInput.x);
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(moveInput.x, moveInput.y, 0f);
        if (force != Vector3.zero)
        {
        ///    Debug.Log("Applying force: " + force);
        }
        rb.AddForce(force * moveForce);
    }

    //*********************************************************************************************************************
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    //*********************************************************************************************************************
    private void LateUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}