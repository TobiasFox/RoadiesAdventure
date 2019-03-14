using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private Queue<string> sentences;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        _audioManager = FindObjectOfType<AudioManager>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextScentence();
        }
    }

    public void StartDielogue(Dialog dialogue)
    {
        Debug.Log("Starting conversation");

        _audioManager.Play("OpenDialogue");
        sentences.Clear();

        _animator.SetBool("IsOpen", true);

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextScentence();
    }

    public void DisplayNextScentence()
    {
       if(sentences.Count == 0)
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
        _dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
        _animator.SetBool("IsOpen", false);
    }
}
