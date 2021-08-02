using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float fromPlayerToCameraDistance = 25f;

    private void Update() {
        transform.position = new Vector3(transform.position.x, player.position.y + 3.72f, player.position.z - fromPlayerToCameraDistance);
    }
}
