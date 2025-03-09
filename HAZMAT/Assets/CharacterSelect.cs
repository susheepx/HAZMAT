using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour
{
    public Transform[] characterPositions; // Positions (Left, Front, Right)
    public GameObject[] characters; // Character GameObjects
    public float moveSpeed = 0.5f; // Speed of transition

    private int currentIndex = 0; // Tracks the front character
    private bool isRotating = false;

    public void RotateCharacters(int direction) // -1 for left, 1 for right
    {
        if (isRotating) return; // Prevent spam clicking
        StartCoroutine(RotateRoutine(direction));
    }

    IEnumerator RotateRoutine(int direction)
    {
        isRotating = true;

        // Shift character references
        if (direction == 1) // Right rotation
        {
            GameObject temp = characters[0];
            for (int i = 0; i < characters.Length - 1; i++)
                characters[i] = characters[i + 1];
            characters[characters.Length - 1] = temp;
        }
        else if (direction == -1) // Left rotation
        {
            GameObject temp = characters[characters.Length - 1];
            for (int i = characters.Length - 1; i > 0; i--)
                characters[i] = characters[i - 1];
            characters[0] = temp;
        }

        float elapsedTime = 0;
        Vector3[] startPos = new Vector3[characters.Length];
        Vector3[] startScale = new Vector3[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            startPos[i] = characters[i].transform.position;
            // startScale[i] = characters[i].transform.localScale;
        }

        while (elapsedTime < moveSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveSpeed;

            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].transform.position = Vector3.Lerp(startPos[i], characterPositions[i].position, t);
                // characters[i].transform.localScale = Vector3.Lerp(startScale[i], characterPositions[i].localScale, t);
            }

            yield return null;
        }

        // Ensure final positioning is exact
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].transform.position = characterPositions[i].position;
            // characters[i].transform.localScale = characterPositions[i].localScale;
        }

        isRotating = false;
    }
}

