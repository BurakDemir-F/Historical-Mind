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
        player = GameObject.FindWithTag("Player").transform;
        return player;

    }
}
