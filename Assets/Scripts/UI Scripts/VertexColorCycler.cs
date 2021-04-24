using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class VertexColorCycler : MonoBehaviour
{
    [SerializeField] Color32 colorOne = new Color32((byte)22, (byte)133, (byte)248, 255);
    [SerializeField] Color32 colorTwo = new Color32((byte)245, (byte)39, (byte)137, 255);
    [SerializeField] private float timeBetweenChanges = .1f;
    private TMP_Text m_TextComponent;

    void Awake()
    {
        m_TextComponent = GetComponentInChildren<TMP_Text>();
    }
    
    private IEnumerator AnimateVertexColors()
    {
        // Force the text object to update right away so we can have geometry to modify right from the start.
        m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        int currentCharacter = 0;

        Color32[] newVertexColors;
        
        
        while (true)
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
                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                
                yield return new WaitForSeconds(timeBetweenChanges);
                
                newVertexColors[vertexIndex + 0] = colorTwo;
                newVertexColors[vertexIndex + 1] = colorTwo;
                newVertexColors[vertexIndex + 2] = colorTwo;
                newVertexColors[vertexIndex + 3] = colorTwo;
                
                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            currentCharacter = (currentCharacter + 1) % characterCount;

            yield return null;
        }
    }
}
