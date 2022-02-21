using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : Singleton<DialogueManager>
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image background;
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

    public bool StartDialogue(Dialogue dialogue)
    {
        GameManager.Instance.ChangeState(GameState.Dialogue);
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name + ": ";
        background.sprite = dialogue.background;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        return DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return false;
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

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
