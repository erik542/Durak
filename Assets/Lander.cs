using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    SceneChanger sceneChanger;
    [SerializeField] float landingScreenTime;

    private void Awake()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    private void Start()
    {
        StartCoroutine(LandScreenDelay());
    }

    IEnumerator LandScreenDelay()
    {
        yield return new WaitForSeconds(landingScreenTime);
        sceneChanger.LoadMainMenu();
    }
}
