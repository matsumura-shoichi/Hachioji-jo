using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class NetworkPlayerSetup : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine)
        {
            // “ü—Í‚ğ~‚ß‚é
            GetComponent<ThirdPersonController>().enabled = false;

            // “ü—ÍƒVƒXƒeƒ€‚à~‚ß‚é
            var input = GetComponent<StarterAssetsInputs>();
            if (input != null)
                input.enabled = false;

            // ƒJƒƒ‰’â~
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
                cam.gameObject.SetActive(false);
        }
    }
}