using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMotor : MonoBehaviour {
    public float speed;
    public float minAngleHoriz = 45f;
    public float minAngleVertically = 45f;
    public Vector2 startDir;
    Rigidbody2D rb;
    Vector3 startingPos;
    Vector2 previousVelocity;
    public Transform aimerPosition;
    GameManager gm;
    AimerController controller;
    public AudioClip impact;
    AudioSource audioSource;






    void Start() {

        audioSource = GetComponent<AudioSource>();
        controller = FindObjectOfType<AimerController>();
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        rb.velocity = transform.up * speed;
    }
    private void Update() {


        if (rb.velocity.magnitude < 0.2f) {
            controller.ballIsInGame = false;
            Destroy(gameObject);
            gm.shots--;
        }
    }
    private void FixedUpdate() {
        previousVelocity = rb.velocity;

    }


    private void OnCollisionExit2D(Collision2D collision) {
        var v = rb.velocity;
        Quaternion rotateClockwise = Quaternion.Euler(0, 0, -minAngleHoriz);
        Quaternion rotateCounterClockwise = Quaternion.Euler(0, 0, minAngleHoriz);
        float angleL = Vector2.Angle(v, Vector2.left);
        if (angleL < minAngleHoriz) {
            if (previousVelocity.y <= 0) {
                print("bounced left, was going down");
                v = rotateClockwise * Vector2.left;
            } else {
                print("bounced left,was going up");
                v = rotateCounterClockwise * Vector2.left;
            }
        }
        float angleR = Vector2.Angle(v, Vector2.right);
        if (angleR < minAngleHoriz) {
            if (previousVelocity.y <= 0) {
                print("bounced right, was going down");
                v = rotateClockwise * Vector2.right;
            } else {
                print("bounced right,was going up");
                v = rotateCounterClockwise * Vector2.right;
            }
            // rb.velocity = v.normalized* speed;
        }


        if (collision.gameObject.CompareTag("Interactable")) {
            audioSource.PlayOneShot(impact, 0.1f);
            print("music exit");
        }
    }
}

   /* private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Interactable")) {
            audioSource.PlayOneShot(impact, 1);
            print("music enter");
        }
    }




}*/
