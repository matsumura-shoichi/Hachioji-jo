using UnityEngine;

[System.Serializable]
public class LocationData
{
    public string locationName;

    [Header("Teleport")]
    public Transform teleportTarget;

    [Header("Info")]
    public Sprite image;
    public AudioClip narration;

    [HideInInspector]
    public bool alreadyVisited = false;

    [Header("Fallââèo")]
    public float dropHeight = 5f;
}