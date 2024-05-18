using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string nameEssentialScene;
    [SerializeField] string nameNewGameStartScene;
    [SerializeField] PlayerData playerData;

    public Gender selectedGender;
    public TMPro.TMP_Text genderText;
    public TMPro.TMP_InputField nameInputField;

    AsyncOperation operation;

    private void Start()
    {
        SetGenderFamale();
        updateName();
    }

    public void ExitGame()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }

    public void StartNewGame()
    {

        SceneManager.LoadScene(nameNewGameStartScene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);
    }

    public void SetGenderMale()
    {
        selectedGender = Gender.Male;
        playerData.playerCharacterGender = selectedGender;
        genderText.text = "Male";
    }

    public void SetGenderFamale()
    {
        selectedGender = Gender.Female;
        playerData.playerCharacterGender = selectedGender;
        genderText.text = "Female";
    }

    public void updateName()
    {
        playerData.characterName = nameInputField.text; 
    }

    public void SetSavingSlot(int num)
    {
        playerData.saveSlotId = num;
    }
}
