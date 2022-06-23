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
        RoomEnterBehaviour.OnRoomEntered += RoomEnteredHandler;
    }

    private void OnDisable()
    {
        RoomEnterBehaviour.OnRoomEntered += RoomEnteredHandler;
    }

    private void RoomEnteredHandler(RoomBehaviour room, Collider col, RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
    {
        // if(MazeInfo.GetRoomData(room).IsBomb)
        // {
        //     SetText("you are dead");
        //     return;
        // }

        if (room.GetRoomPosition() == MazeGenerator.Instance.GoalCell.Position)
        {
            SetText("you win");
            return;
        }

        // var neighbors = MazeInfo.GetNeighborRooms(room.GetRoomPosition());
        // var counter = 0;
        // foreach (var neighbor in neighbors)
        // {
        //     if (MazeInfo.GetRoomData(neighbor).IsBomb)
        //     {
        //         counter++;
        //     }
        // }
        //
        // SetText($"Dangerous room count: {counter}");

    }
}
