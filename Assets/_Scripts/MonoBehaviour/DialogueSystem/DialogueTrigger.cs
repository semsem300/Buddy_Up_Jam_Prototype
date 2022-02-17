using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] Dialogue dialogue;
    //[SerializeField] GameObject transform;
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] Animator animator;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enter");
            TriggerDialogue();
            if (transform != null)
            {
                Instantiate(particleSystem, transform.transform.position, Quaternion.identity);
                animator.SetTrigger("Heal");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
