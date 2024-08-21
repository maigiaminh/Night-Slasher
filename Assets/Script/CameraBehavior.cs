using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using EZCameraShake;

public class CameraBehavior : MonoBehaviour
{
  public GameObject player;
  private Vector3 velocity = Vector3.zero;
  public float smoothTime = 0.2f;
  private Vector3 mousePos;

  void Update()
  {
    mousePos = Input.mousePosition;
  }
  void LateUpdate()
  {
    mousePos = new Vector3(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2, 0);
    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 3f, -10f) + mousePos * 0.004f, ref velocity, smoothTime);
  }

  public void ShakeCamera()
  {
    StartCoroutine(Shake(0.3f, 0.05f));
  }

  IEnumerator Shake(float duration, float magnitude)
  {
    Vector3 OriginalPos = transform.localPosition;
    float elapsed = 0.0f;
    while (elapsed < duration)
    {
      float x = transform.localPosition.x + Random.Range(-1f, 1f) * magnitude;
      float y = transform.localPosition.y + Random.Range(-1f, 1f) * magnitude;

      transform.localPosition = new Vector3(x, y, OriginalPos.z);

      elapsed += Time.deltaTime;
      yield return null;
    }
  }
}
