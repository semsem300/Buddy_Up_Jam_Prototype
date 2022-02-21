using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : Singleton<DialogueManager>
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image DialogueBackGround;
    public Animator animator;

    private Queue<string> sentences;
    [SerializeField]
    [Range(0, 1)]
    float waitBetweenCharacters = .1f;
    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(List<Dialogue> dialogues)
    {
        GameManager.Instance.ChangeState(GameState.Dialogue);
        animator.SetBool("IsOpen", true);

        sentences.Clear();
        foreach (Dialogue dialogue in dialogues)
        {
            nameText.text = dialogue.Speeker + ": ";
            DialogueBackGround.sprite = dialogue.Pic;
            sentences.Enqueue(dialogue.sentences);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(waitBetweenCharacters);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
