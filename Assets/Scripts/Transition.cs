using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum TransitionType
{
    Warp,
    Scene
}
public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Collider2D confiner;

    CameraConfiner cameraConfiner;
    [SerializeField] Transform destination;

    private void Start()
    {
        if (confiner != null)
        {
            cameraConfiner = FindObjectOfType<CameraConfiner>();

        }
    }

    internal void InitiateTransition(Transform toTransition)
    {
        switch (transitionType)
        {
            case TransitionType.Warp:
                Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

                if (cameraConfiner != null)
                {
                    cameraConfiner.UpdateBounds(confiner);
                }

                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
                    toTransition, destination.position - toTransition.position
                    );
                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);



                break;
            case TransitionType.Scene:
                GameSceneManager.instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (transitionType == TransitionType.Scene)
        {
            Handles.Label(transform.position, "to " + sceneNameToTransition);
        }

        if (transitionType == TransitionType.Warp)
        {
            Gizmos.DrawLine(transform.position, destination.position);
        }
    }
}