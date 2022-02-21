using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] List<Dialogue> dialogues;
    //[SerializeField] GameObject transform;
    [SerializeField] Animator animator;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogues);
    }
    private void Start()
    {
        TriggerDialogue();
    }
  
}
