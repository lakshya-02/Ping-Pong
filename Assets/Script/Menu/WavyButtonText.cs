using UnityEngine;
using TMPro;

public class SingleLetterWavyText : MonoBehaviour
{
    public TMP_Text buttonText;
    public float waveSpeed = 2f; // Speed of the wave motion
    public float waveHeight = 2f; // Height of the wave

    void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>();
        }
        buttonText.ForceMeshUpdate();
    }

    void Update()
    {
        TMP_TextInfo textInfo = buttonText.textInfo;
        int characterCount = textInfo.characterCount;

        if (characterCount == 0) return;

        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Apply a wave effect to each letter, making it move in place
            float offset = Mathf.Sin(Time.time * waveSpeed + i * 0.5f) * waveHeight;

            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j].y += offset;
            }
        }

        // Update the mesh with the modified vertices
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            buttonText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}
