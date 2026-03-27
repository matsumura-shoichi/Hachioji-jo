using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.NickName = "Player_" + Random.Range(1000, 9999);

        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        // ランダムルームに入る（なければ作る）
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found. Creating new room...");

        PhotonNetwork.CreateRoom(
            null,
            new RoomOptions { MaxPlayers = 5 }
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);

        Vector3 spawnPos = new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3));

        PhotonNetwork.Instantiate("NetworkPlayer", spawnPos, Quaternion.identity);
    }
}