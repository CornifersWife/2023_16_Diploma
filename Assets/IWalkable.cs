using UnityEngine;

public interface IWalkable {
    void SetTargetPoint();
    void Move(Vector3 target);
}
