
using UnityEngine;

public class MFMoveComponent : MonoBehaviour {
    public float speed = 10f;

    public void Move(float moveX, float moveY, float moveZ) {
        Vector3 movement = new Vector3(moveX * speed, moveY * speed, moveZ * speed);
        transform.Translate(movement);
    }
}
