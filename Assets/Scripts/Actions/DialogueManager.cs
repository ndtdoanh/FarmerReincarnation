using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private static DialogueManager instance;
    private Story currentStory;
    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private List<Choice> originalChoices = new List<Choice>();
    public bool dialoguePlaying { get; private set; }

    // Thêm biến cờ để kiểm tra xem văn bản đang được hiển thị từng từ một
    private bool isTyping = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialoguePlaying)
        {
            return;
        }

        // Chỉ tiếp tục khi văn bản hiện tại đã được hiển thị hết và không có lựa chọn nào đang hiển thị
        if (InputManager.GetInstance().GetSubmitPressed() && !isTyping && !AreChoicesDisplayed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (inkJSON != null)
        {
            currentStory = new Story(inkJSON.text);
            dialoguePlaying = true;
            dialoguePanel.SetActive(true);
            ContinueStory();
        }
        else
        {
            Debug.LogError("inkJSON is null.");
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory != null)
        {
            if (currentStory.canContinue)
            {
                string nextText = currentStory.Continue();
                StopAllCoroutines();  // Dừng tất cả các coroutine hiện tại
                StartCoroutine(DisplayTextOneByOne(nextText));
                DisplayChoices();
            }
            else
            {
                StartCoroutine(ExitDialogueMode());
            }
        }
        else
        {
            Debug.LogError("currentStory is null.");
        }
    }

    private IEnumerator DisplayTextOneByOne(string text)
    {
        dialogueText.text = "";  // Xóa văn bản hiện tại
        isTyping = true;  // Bắt đầu hiển thị từng từ một

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;  // Thêm từng ký tự vào hộp thoại
            yield return new WaitForSeconds(0.05f);  // Thời gian trễ giữa các ký tự (0.05 giây)
        }

        isTyping = false;  // Kết thúc hiển thị từng từ một
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        Debug.Log("Displaying " + currentChoices.Count + " choices.");

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            Debug.Log("Choice " + index + ": " + choice.text);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;

            Button button = choices[index].GetComponent<Button>();
            if (button != null)
            {
                int choiceIndex = index;  // Local copy of index for the closure
                button.onClick.RemoveAllListeners();  // Clear existing listeners
                button.onClick.AddListener(() => MakeChoice(choiceIndex));  // Add new listener
            }

            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (choiceIndex >= 0 && choiceIndex < currentChoices.Count)
        {
            Debug.Log("Making choice: " + choiceIndex);
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    private bool AreChoicesDisplayed()
    {
        // Kiểm tra xem có lựa chọn nào đang hiển thị hay không
        foreach (GameObject choice in choices)
        {
            if (choice.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
