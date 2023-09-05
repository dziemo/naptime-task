using System.Collections;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    private Coroutine rotationCoroutine;

    public void StartRotation()
    {
        rotationCoroutine = StartCoroutine(RotationSequence());
    }

    public void StopRotation()
    {
        StopCoroutine(rotationCoroutine);
    }

    private IEnumerator RotationSequence()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        Rotate();
        rotationCoroutine = StartCoroutine(RotationSequence());
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, Random.Range(0f, 360f));
    }
}
