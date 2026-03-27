using UnityEngine;
using Photon.Pun;

public class CubeMove : MonoBehaviourPun
{
    void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * 5f * Time.deltaTime);
    }
}