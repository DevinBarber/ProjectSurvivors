using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObjScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform player;
    [SerializeField] PlayerLookAtMouseScript lookAtMouseScript;
    private Transform m_transform;
    private Vector3 mousePos;
    private Vector3 cameraTargetPos;
    [Range(2, 100)][SerializeField] private float cameraTargetDivider;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtMouseScript.isGamepad)
        {
            m_transform.position = player.transform.position;
        } else
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            cameraTargetPos = (mousePos + (cameraTargetDivider - 1) * player.position) / cameraTargetDivider;
            m_transform.position = cameraTargetPos;
        }
    }
}
