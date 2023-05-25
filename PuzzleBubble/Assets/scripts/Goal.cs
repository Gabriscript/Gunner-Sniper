using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject stars;
    public  bool goal = false;
    AimerController controller;
    public AudioClip goalSound;
    AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        controller = FindObjectOfType<AimerController>();
        stars.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
       if(collision.CompareTag("Ball")) {
            Destroy(collision.gameObject);
            controller.ballIsInGame = false;
            print("You Win");
            stars.SetActive(true);
            audioSource.PlayOneShot(goalSound,0.5f);
            Invoke("DisableParticle", 1);
        }
       
    }
    void DisableParticle() {

            goal = true;
        stars.SetActive(false); 
    }
}
