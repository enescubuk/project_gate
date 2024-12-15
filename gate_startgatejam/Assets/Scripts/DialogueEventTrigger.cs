using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueEventTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private List<string> dialogueTexts; // Diyalog listesi
    [SerializeField] private float typingSpeed = 0.05f; // Harf yazma hızı
    [SerializeField] private float dialogueDuration = 2f; // Her diyalog arasında bekleme süresi
    [SerializeField] private AudioClip dialogueClip; // Diyalog sesi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            EventTriggered();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void EventTriggered()
    {
        dialogueCanvas.SetActive(true);
        PlaySound();
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        TMP_Text dialogueTextComponent = dialogueCanvas.GetComponentInChildren<TMP_Text>();

        foreach (string sentence in dialogueTexts)
        {
            yield return StartCoroutine(TypeSentence(dialogueTextComponent, sentence)); // Harf harf yaz
            yield return new WaitForSeconds(dialogueDuration); // Diyalog sonrası bekleme süresi
        }

        dialogueCanvas.SetActive(false); // Tüm diyaloglar bittiğinde canvas kapatılır
    }

    private IEnumerator TypeSentence(TMP_Text dialogueTextComponent, string sentence)
    {
        dialogueTextComponent.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Harf arası yazma hızı
        }
    }

    private void PlaySound()
    {
        if (dialogueClip != null)
        {
            SoundManager.Instance.PlayDialogue(dialogueClip);
        }
    }
}
