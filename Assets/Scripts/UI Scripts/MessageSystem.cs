using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] Color32 colorOne = new Color32((byte)22, (byte)133, (byte)248, 255);
    [SerializeField] Color32 colorTwo = new Color32((byte)245, (byte)39, (byte)137, 255);
    [Range(0.01f, 1)] [SerializeField] private float timeBetweenChanges = .1f;
    [Range(0.1f, 1)] [SerializeField] private float restBetweenMessage = .5f;
    private int numberOfTimesToDisplayMessage = 3;
    private int currentTimesDisplayed = 0;
    [SerializeField] float maxYMotion = 15f;
    [SerializeField] float maxXMotion = 1f;
    [SerializeField] float maxZoom = 1f;

    [SerializeField] private TMP_Text textComponent;
    private bool messageISActive = false;
    private TMP_TextInfo textInfo;
    
    void Awake()
    {
        textComponent = GetComponentInChildren<TMP_Text>();
    }

    public void DisplayMessage(string message, int numberOfTimes)
    {
        StopMessage();
        numberOfTimesToDisplayMessage = numberOfTimes;
        textComponent.text = message;
        messageISActive = true;
        textComponent.enabled = messageISActive;
        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;
        StartCoroutine(AnimateVertexColors());
        StartCoroutine(ZoomCharacters());
    }

    public void StopMessage()
    {
        currentTimesDisplayed = 0;
        messageISActive = false;
        textComponent.enabled = messageISActive;
    }
    
    private IEnumerator AnimateVertexColors()
    {
        int currentCharacter = 0;

        Color32[] newVertexColors;
        
        
        while (messageISActive)
        {
            int characterCount = textInfo.characterCount;

            // If No Characters then just yield and wait for some text to be added
            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }

            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

            // Only change the vertex color if the text element is visible.
            if (textInfo.characterInfo[currentCharacter].isVisible)
            {
                newVertexColors[vertexIndex + 0] = colorOne;
                newVertexColors[vertexIndex + 1] = colorOne;
                newVertexColors[vertexIndex + 2] = colorOne;
                newVertexColors[vertexIndex + 3] = colorOne;
                
                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                
                yield return new WaitForSeconds(timeBetweenChanges);
                
                newVertexColors[vertexIndex + 0] = colorTwo;
                newVertexColors[vertexIndex + 1] = colorTwo;
                newVertexColors[vertexIndex + 2] = colorTwo;
                newVertexColors[vertexIndex + 3] = colorTwo;
                
                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            currentCharacter = (currentCharacter + 1) % characterCount;
            if (currentCharacter == 0)
            {
                yield return new WaitForSeconds(restBetweenMessage);
                CheckForMessageTimeout();
            }
            else
                yield return null;
        }
    }
    
    private IEnumerator ZoomCharacters()
    {
        //TMP_TextInfo textInfo = textComponent.textInfo;
        int currentCharacter = 0;

        Matrix4x4 matrix;
        TMP_MeshInfo[] cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();

        // Allocations for sorting of the modified scales
        //List<float> modifiedCharScale = new List<float>();

        while (messageISActive)
        {
            int characterCount = textInfo.characterCount;

            // If No Characters then just yield and wait for some text to be added
            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }
            
            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

            // Get the cached vertices of the mesh used by this text element (character or sprite).
            Vector3[] sourceVertices = cachedMeshInfoVertexData[materialIndex].vertices;

            // Determine the center point of each character.
            Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;

            // Need to translate all 4 vertices of each quad to aligned with middle of character / baseline.
            // This is needed so the matrix TRS is applied at the origin for each character.
            Vector3 offset = charMidBasline;

            Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

            if (textInfo.characterInfo[currentCharacter].isVisible)
            {
                destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;

                // Setup the matrix for the scale change.
                matrix = Matrix4x4.TRS(new Vector3(1 * maxXMotion, 1 * maxYMotion, 1), Quaternion.identity, Vector3.one * maxZoom);

                destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
                destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
                destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
                destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);

                destinationVertices[vertexIndex + 0] += offset;
                destinationVertices[vertexIndex + 1] += offset;
                destinationVertices[vertexIndex + 2] += offset;
                destinationVertices[vertexIndex + 3] += offset;

                // Updated modified vertex attributes
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                textInfo.meshInfo[materialIndex].mesh.uv = textInfo.meshInfo[materialIndex].uvs0;

                textComponent.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
                
                yield return new WaitForSeconds(timeBetweenChanges);
                
                destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;
                
                matrix = Matrix4x4.TRS(new Vector3(1, 1, 1), Quaternion.identity, Vector3.one);

                destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
                destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
                destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
                destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);

                destinationVertices[vertexIndex + 0] += offset;
                destinationVertices[vertexIndex + 1] += offset;
                destinationVertices[vertexIndex + 2] += offset;
                destinationVertices[vertexIndex + 3] += offset;
                
                // Updated modified vertex attributes
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                textInfo.meshInfo[materialIndex].mesh.uv = textInfo.meshInfo[materialIndex].uvs0;

                textComponent.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
            }
            
            currentCharacter = (currentCharacter + 1) % characterCount;

            if (currentCharacter == 0)
               yield return new WaitForSeconds(restBetweenMessage);
            else
                yield return null;
        }
    }

    private void CheckForMessageTimeout()
    {
        currentTimesDisplayed++;
        if(currentTimesDisplayed == numberOfTimesToDisplayMessage)
            StopMessage();
    }
}
