using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameTag : MonoBehaviour
{
    public TMP_Text nameText;

    void Start()
    {
        PhotonView pv = GetComponentInParent<PhotonView>();

        nameText.text = pv.Owner.NickName;

        if (pv.IsMine)
        {
            nameText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}