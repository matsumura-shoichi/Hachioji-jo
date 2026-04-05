using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonLoginUI : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;
    public GameObject loginCanvas;
    public TMP_Text errorText;
    public GameObject mobileUI; // Joystickなど

    void Start()
    {
        errorText.text = "ロボットに名前を付けてください（例：氏照、兼続　etc. ）";

        if (mobileUI != null)
            mobileUI.SetActive(false); // 最初は非表示
    }

    public void ConnectToPhoton()
    {
        Debug.Log("ボタンが押されました");

        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("名前が空です");
            errorText.text = "名前を入力してください";
            return;
        }

        // 名前登録
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.AuthValues = new AuthenticationValues(playerName);

        Debug.Log("Photon接続開始 : " + PhotonNetwork.NickName);

        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("接続関数呼び出し完了");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Masterサーバー接続");
        StartCoroutine(JoinRoomDelay());
    }

    System.Collections.IEnumerator JoinRoomDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("ランダムルーム参加");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("部屋がないので作成");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルーム参加成功");

        loginCanvas.SetActive(false);

        if (mobileUI != null)
            mobileUI.SetActive(true); // ゲーム開始で表示

        // ① プレイヤー生成
        GameObject player = SpawnPlayer();

        // ② オープニング開始
        FindObjectOfType<OpeningManager>().StartOpening(player);
    }

    GameObject SpawnPlayer()
    {
        Vector3 basePos = new Vector3(470, 250, 495);
        Vector3 offset = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        Vector3 pos = basePos + offset;

        // 色
        int colorIndex = Random.Range(0, 5);

        // ★ スケール
        float scaleY = Random.Range(0.9f, 1.0f);
        float scaleXZ = Random.Range(0.8f, 1.2f);

        object[] data = new object[] { colorIndex, scaleY, scaleXZ };

        GameObject player = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity, 0, data);

        PhotonNetwork.LocalPlayer.TagObject = player;

        return player;
    }
}