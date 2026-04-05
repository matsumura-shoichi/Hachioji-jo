using Photon.Pun;
using UnityEngine;

public class PlayerAppearance : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public Color[] colors;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = photonView.InstantiationData;

        int colorIndex = (int)data[0];
        float scaleY = (float)data[1];
        float scaleXZ = (float)data[2];

        // ===== 色変更 =====
        if (colors.Length > colorIndex)
        {
            Color color = colors[colorIndex];

            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                Material[] mats = r.materials;

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = new Material(mats[i]);
                    mats[i].color = color;
                }

                r.materials = mats;
            }
        }

        // ===== サイズ変更 =====
        transform.localScale = new Vector3(scaleXZ, scaleY, scaleXZ);
    }
}