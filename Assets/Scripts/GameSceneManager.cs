using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] ScreenTint screenTint;
    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to,targetPosition));
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f); //1 second divined  by speed of tinting and small addition of time offset

        SwitchScene(to, targetPosition);

        yield return new WaitForEndOfFrame();

        screenTint.UnTint();
    }


    public void SwitchScene(string to, Vector3 targetPosition)
    {

        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);    
        currentScene = to;  
        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            playerTransform, 
            targetPosition - playerTransform.position
            );

        playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);   
    }
}
