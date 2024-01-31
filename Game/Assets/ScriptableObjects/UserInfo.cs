using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo",menuName = "User")]
public class UserInfo : ScriptableObject
{
    public int Id;
    public string Username;
    public string Token;
    public int TeamId;
    public string Role;
}
