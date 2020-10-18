using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    public Material[] tutorial;
    public Material[] progress;
    public Material[] help;
    public GameObject tutorial_board, progress_board, help_board;
    public static bool TrainingMode; // Режим обучения. Если True то установленные компоненты нельзя снять
    public VRTK_InteractableObject[] Components;
    
    int k = 0, k_help = 0;
    private static int _progress;
    
    // Вызывается после установки компонента
    public static void AddProgressStep()
    {
        _progress++;
    }
    
    // Вызывается после извлечения компонента
    public static void RemoveProgressStep()
    {
        _progress--;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && k < 7)
        {
            k++;
            Transition();
        }
    }

    public void Restart()
    {
        k = 0;
        Transition();
    }

    void Transition()
    {
        tutorial_board.GetComponent<Renderer>().material = tutorial[k];
        progress_board.GetComponent<Renderer>().material = progress[k];
        if (k == 3 || k == 5) help_board.GetComponent<Renderer>().material = help[++k_help];
        else if (k == 0)
        {
            k_help = 0;
            help_board.GetComponent<Renderer>().material = help[k_help];
        }
    }
}
