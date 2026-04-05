using Photon.Pun;
using UnityEngine;

public class PlayerColor : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public Color[] colors;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = photonView.InstantiationData;

        int colorIndex = (int)data[0];

        if (colors.Length <= colorIndex) return;

        Color color = colors[colorIndex];

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            // ★ 全マテリアル取得
            Material[] mats = r.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = new Material(mats[i]); // 個別化
                mats[i].color = color;
            }

            r.materials = mats;
        }
    }
}