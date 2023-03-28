using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField sensInput;
    [SerializeField] private GameObject optionsMenu;
    
    private FloatVal sens;
    
    private void Awake()
    {
        sens = Resources.Load<FloatVal>("ScriptableObjects/sensVal");
    }
    
    private void Start()
    {
        if(optionsMenu.activeSelf)
            ShowOptions(); // Hides Options Menu
        sensInput.placeholder.GetComponent<TextMeshProUGUI>().SetText(sens.val.ToString()); // Sets sens input field to current sens
    }
    
    private void OnEnable()
    {
        sensInput.onValueChanged.AddListener(delegate {ChangeSens(sensInput);  }); // Event to change sens
        PlayerController.showOptions += ShowOptions; // Shows Options menu
    }

    private void OnDisable()
    {
        sensInput.onValueChanged.AddListener(delegate { ChangeSens(sensInput); }); // Event to change sens
        PlayerController.showOptions -= ShowOptions; // Shows Options menu
    }

    private void ShowOptions()
    {
        Cursor.lockState = optionsMenu.activeSelf ? CursorLockMode.Locked : Cursor.lockState = CursorLockMode.None; // Detects whether it shows or hides cursor
        optionsMenu.SetActive(!optionsMenu.activeSelf); // Hides or shows options menu
    }
    
    private void ChangeSens(TMP_InputField newSens)
    {
        float.TryParse(newSens.text, out var sensitivity); // Converts user input to float
        sens.val = sensitivity; // sets new sens
    }
}
