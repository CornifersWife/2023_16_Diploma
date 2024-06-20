using System.Collections;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces {
    public interface IMoveable {
        public IEnumerator Move(Vector3 vector3);
    }
}