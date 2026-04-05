using UnityEngine;
using StarterAssets;
using Photon.Pun;
using Cinemachine;
using UnityEngine.EventSystems;

public class MyMobileInput : MonoBehaviour
{
    StarterAssetsInputs input;
    PhotonView pv;

    Joystick moveJoystick;

    [Header("Camera")]
    public float lookSensitivity = 0.3f;

    [Header("Zoom")]
    public CinemachineVirtualCamera vcam;
    public float zoomSpeed = 0.01f;
    public float minDistance = 2f;
    public float maxDistance = 100f;

    Cinemachine3rdPersonFollow follow;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        pv = GetComponent<PhotonView>();

        // ★ シーン内のJoystickを自動取得
        moveJoystick = FindObjectOfType<Joystick>();

        // ★ カメラ取得（自分のプレイヤーのみ）
        if (pv.IsMine)
        {
            vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        if (vcam != null)
        {
            follow = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
    }

    void Update()
    {
        if (input == null) return;
        if (pv != null && !pv.IsMine) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        // ===== PC操作 =====
        input.move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return;
#endif

#if UNITY_ANDROID || UNITY_IOS
        // ===== Joystick再取得（念のため）
        if (moveJoystick == null)
        {
            moveJoystick = FindObjectOfType<Joystick>();
            return;
        }

        // ===== 移動 =====
        input.move = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);

        // ===== カメラ =====
        input.look = Vector2.zero;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // UIの上は無視（Joystick以外）
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                continue;

            if (touch.position.x > Screen.width * 0.5f)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    input.look += touch.deltaPosition * lookSensitivity;
                }
            }
        }

        // ===== ピンチズーム =====
        if (Input.touchCount == 2 && follow != null)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDist =
                (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;

            float currentDist = (t0.position - t1.position).magnitude;

            float delta = currentDist - prevDist;

            float distance = follow.CameraDistance;
            distance -= delta * zoomSpeed;

            follow.CameraDistance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
#endif
    }
}