using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject UI;

    public bool IsInDialogue { get; private set; } = false;

    private void Awake()
    {
        UI.SetActive(true);

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject); 
    }

    public void EnterDialogue() // ¥Î»≠æ¿ªÛ≈¬
    {
        IsInDialogue = true;
    }

    public void ExitDialogue() // ¥Î»≠æ¿¡æ∑·
    {
        IsInDialogue = false;
    }
}
