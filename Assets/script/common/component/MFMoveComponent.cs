
using UnityEngine;

public class MFMoveComponent : MonoBehaviour {
    public float speed = 10f;

    public void Move(float moveX, float moveY, float moveZ) {
        Vector3 movement = new Vector3(moveX * speed, moveY * speed, moveZ * speed);

        #region debug模式下显示射线
        if (MFApplicationUtil.IsOpenDebug()) {
            RaycastHit dhit;
            if (Physics.Raycast(new Ray(transform.position, movement), out dhit))
                Debug.DrawLine(transform.position, dhit.point, Color.red);
        }
        #endregion

        transform.Translate(movement);
    }

    public void SpeedUp(float addValue) {
        speed += addValue;
    }
}
