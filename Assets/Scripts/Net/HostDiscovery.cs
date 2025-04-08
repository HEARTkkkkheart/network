using System.Net;
using System.Net.Sockets;
using Unity.Netcode;
using UnityEngine;

public class HostDiscovery : MonoBehaviour {
    [SerializeField] private string broadcastMessage = "MyGameHost";
    private UdpClient _udpClient;

    void Start() {
        _udpClient = new UdpClient();
        _udpClient.EnableBroadcast = true;
        // 每隔3秒广播一次
        InvokeRepeating(nameof(BroadcastHost), 0, 3f);
    }

    void BroadcastHost() {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(broadcastMessage);
        _udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, 12345));
    }
}
