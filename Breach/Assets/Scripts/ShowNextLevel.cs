using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelButton;
    private void Start()
    {
        if (!GameManager.instance.IsThisTheLastLevel())
        {
            nextLevelButton.SetActive(true);
        }
        else
        {
            nextLevelButton.SetActive(false);
        }
    }

}
