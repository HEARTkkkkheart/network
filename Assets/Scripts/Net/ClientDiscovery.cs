using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientDiscovery : MonoBehaviour {
    private UdpClient _udpClient;
    private List<string> _foundHosts = new List<string>();

    void Start() {
        _udpClient = new UdpClient(12345);
        _udpClient.BeginReceive(OnBroadcastReceived, null);
    }

    private void OnBroadcastReceived(IAsyncResult result) {
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        byte[] data = _udpClient.EndReceive(result, ref remoteEP);
        string hostIP = remoteEP.Address.ToString();
        if (!_foundHosts.Contains(hostIP)) {
            _foundHosts.Add(hostIP);
            Debug.Log("发现主机: " + hostIP);
        }
        _udpClient.BeginReceive(OnBroadcastReceived, null);
    }
}
