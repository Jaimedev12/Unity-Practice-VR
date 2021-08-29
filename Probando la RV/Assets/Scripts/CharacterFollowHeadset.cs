using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterFollowHeadset : MonoBehaviour
{
    public float heightBoost = 0.2f;

    private XRRig rig;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<XRRig>();
        character = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        CapsuleFollowHeadset();
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + heightBoost;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, capsuleCenter.y/2 + character.skinWidth , capsuleCenter.z);
    }
}
