using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class VertexZoom : MonoBehaviour
    {
        private TMP_Text m_TextComponent;
        [SerializeField] float maxYScaler = 15f;
        [SerializeField] private float timeBetweenChanges = .1f;
        
        void Awake()
        {
            m_TextComponent = GetComponentInChildren<TMP_Text>();
        }
        
        
        private IEnumerator ZoomCharacters()
        { 
            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            m_TextComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            int currentCharacter = 0;

            Matrix4x4 matrix;
            TMP_MeshInfo[] cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();

            // Allocations for sorting of the modified scales
            //List<float> modifiedCharScale = new List<float>();

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
                    matrix = Matrix4x4.TRS(new Vector3(0, 1 * maxYScaler, 0), Quaternion.identity, Vector3.one);

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

                    m_TextComponent.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
                    
                    yield return new WaitForSeconds(timeBetweenChanges);
                    
                    destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                    destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                    destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                    destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;
                    
                    matrix = Matrix4x4.TRS(new Vector3(0, 1, 0), Quaternion.identity, Vector3.one);

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

                    m_TextComponent.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
                }
                
                currentCharacter = (currentCharacter + 1) % characterCount;
                
                //if (currentCharacter == 0)
                    //SwitchToggle();
                
                yield return null;
            }
        }
        
        //private void SwitchToggle()
        //{
        //    toggle = !toggle;
        //    if (toggle)
        //    {
        //        maxYScaler = 15f;
        //    }
        //    else
        //    {
        //        maxYScaler = 1f;
        //    }
        //}
    }
