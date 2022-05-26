using System;
using System.Collections;
using System.Collections.Generic;
using Maze;
using UnityEngine;

public class TextInfoTest : MonoBehaviour
{
    [SerializeField]private TMPro.TMP_Text infoText;

    public void SetText(string text)
    {
        infoText.SetText(text);
    }

    private void OnEnable()
    {
        RoomBehaviour.onRoomEntered += RoomEnteredHandler;
    }

    private void OnDisable()
    {
        RoomBehaviour.onRoomEntered += RoomEnteredHandler;
    }

    private void RoomEnteredHandler(RoomBehaviour room, Collider col)
    {
        if(room.IsBombRoom)
        {
            SetText("you are dead");
            return;
        }

        var neighbors = MazeInfo.GetNeighborRooms(room.GetRoomPosition());
        var counter = 0;
        foreach (var neighbor in neighbors)
        {
            if (neighbor.IsBombRoom)
            {
                counter++;
            }
        }
        
        SetText($"Dangerous room count: {counter}");

    }
}
