using UnityEngine;

public interface IWalkable {
    void SetTargetPoint();
    void Walk(Vector3 target);
}
