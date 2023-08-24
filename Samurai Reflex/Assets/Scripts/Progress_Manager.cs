using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Health;
    public int Armor;
}

public class Progress_Manager : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public static Progress_Manager Instance;


}
