using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AimerController : MonoBehaviour
{
     Transform m_Transform;
    Camera m_Camera;
    public int rotationOffset = 0;
    float holdStartTime = 0;
    public float currentTime;
    public Transform bubbleSpawnPosition;
    BubbleMotor bubbleMotor  ;
    public GameObject bubble;
     Powerbar powerBar;
    public bool ballIsInGame = false;
    public GameObject smoke;
    GameManager gm;
    public AudioClip shot;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameManager>();
        powerBar = FindObjectOfType<Powerbar>();
        bubbleMotor = FindObjectOfType<BubbleMotor>();
        m_Transform = transform;
        m_Camera = Camera.main;
      currentTime = holdStartTime;
       powerBar.SetMaxPower(holdStartTime);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

   public  void LAMouse() {
        Vector2 direction =  m_Camera.ScreenToWorldPoint
            (Input.mousePosition) - m_Transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y)*
            Mathf.Rad2Deg;//Angle rotate aroud to face cursor
        m_Transform.rotation = Quaternion.Euler(0f,0f,angle + rotationOffset);
    }
    
    private void Update() {

        if (gm.gameEnded == false || gm.gamePaused == false) {
            if (ballIsInGame == false) {
            LAMouse();
                spriteRenderer.color = Color.white;
                if (Input.GetKey(KeyCode.Mouse0)) {

                    if (currentTime < 4) {
                        print("time is running");
                        currentTime += Time.deltaTime;
                        powerBar.SetPower(currentTime);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Mouse0)) {




                    Shoot(currentTime);
                    Invoke("DisableParticle", 1);
                }
            }
            if (ballIsInGame) {
                spriteRenderer.color = Color.red;
            }
        }

    }

    void Shoot(float power) {
        ballIsInGame = true;
        GameObject spawnedBubble = Instantiate(bubble, bubbleSpawnPosition.transform.position, bubbleSpawnPosition.rotation);
        spawnedBubble.GetComponent<BubbleMotor>().speed +=power * 3;
       currentTime = holdStartTime;
        powerBar.SetPower(currentTime);
        smoke.SetActive(true);
        audioSource.PlayOneShot(shot, 0.1f);
    }

    private void DisableParticle() {
        smoke.SetActive(false);
    }
}
