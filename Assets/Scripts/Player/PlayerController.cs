using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour {
    private NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();

    void Update() {
        if (IsOwner) {
            // 本地玩家移动
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime;
            RequestPositionUpdateServerRpc(transform.position);
        }
    }

    [ServerRpc]
    void RequestPositionUpdateServerRpc(Vector3 newPos) {
        position.Value = newPos;
    }
}
