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
            var controller = GetComponent<ThirdPersonController>();
            if (controller != null)
                controller.enabled = false;

            // “ü—ÍƒVƒXƒeƒ€’â~
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