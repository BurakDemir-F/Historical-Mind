using System.Collections;
using System.Collections.Generic;
using MazeWorld.Npc;
using UnityEngine;

public class TestTargetGetter : MonoBehaviour,IGetTargetTransform
{
    [SerializeField] private Transform player;
    public Transform GetTarget()
    {
        if (player != null) return player;
        var playerObject = GameObject.FindWithTag("Player");
        var col = playerObject.GetComponentInChildren<Collider>();
        player = col.transform;
        return player;

    }
}
