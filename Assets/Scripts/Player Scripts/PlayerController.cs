using System.Collections;
using System.Collections.Generic;
using Ash.MyUtils;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    public PlayerCharacter[] availableCharacters;
    public PlayerCharacter controlledCharacter;
    public PlayerCharacter busyCharacter;
    public CameraController cameraController;
    
    private Vector2 directionAxis;

    private int currentCharacterIndex = 0;
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Swap.performed += cxt => SwapCharacters();
        playerControls.Gameplay.Move.performed += cxt => SetMovement(cxt.ReadValue<Vector2>());
        playerControls.Gameplay.Move.canceled += cxt => ResetMovement();
        controlledCharacter = availableCharacters[currentCharacterIndex];
        busyCharacter = availableCharacters[currentCharacterIndex + 1];
        controlledCharacter.PlayerSelected();
        cameraController.AssignTarget(controlledCharacter.transform);
    }

    private void Update()
    {
        if (controlledCharacter != null)
        {
            controlledCharacter.PlayerCharacterMove(directionAxis);
        }
    }

    private void SwapCharacters()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex > availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        }

        controlledCharacter.PlayerDeselected();
        busyCharacter = controlledCharacter;
        controlledCharacter = availableCharacters[currentCharacterIndex];
        controlledCharacter.PlayerSelected();
        cameraController.AssignTarget(controlledCharacter.transform);  
    }

    private void SetMovement(Vector2 movement) => directionAxis = movement;
	
    private void ResetMovement() => directionAxis = Vector3.zero;
}
