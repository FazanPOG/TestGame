using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInputField;
    [SerializeField] private InputField joinInputField;

    public void CreateRoom() 
    {
        PhotonNetwork.CreateRoom(createInputField.text);
    }

    public void JoinRoom() 
    {
        PhotonNetwork.JoinRoom(joinInputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
