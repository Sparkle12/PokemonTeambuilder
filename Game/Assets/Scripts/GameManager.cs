using Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;
using SharedLibrary.Requests;
using Newtonsoft.Json;
using SharedLibrary.Responses;
using UnityEngine.SceneManagement;
using SharedLibrary.Models.DTOs;

public class LoginManager : MonoBehaviour
{
    private string username = "";
    private string password = "";
    private bool usernameNull;
    private bool passwordNull;
    public Text error;
    public UserInfo user;
    async void Start()
    {

    }

    public void UsernameNotNull(string u)
    {
        if (u == null)
            usernameNull = true;
        else
        {
            usernameNull = false;
            username = u;
        }
    }
    public void PasswordNotNull(string p)
    {
        if (p == null)
            passwordNull = true;
        else
        {
            passwordNull = false;
            password = p;
        }
    }
    public async void Login()
    {
        if (passwordNull || usernameNull)
            error.text = "Cannot leave fields empty.";
        else
        {
            AuthenticationRequest request = new AuthenticationRequest();
            request.Username = username;
            request.Password = password;
            var response = await MyHttpClient.Post<AuthenticationResponse>("https://localhost:7022/authentication/login", request);
            if (response.Token.Contains("Invalid"))
                error.text = response.Token;
            else
            {
                error.text = "";
                var dto = await MyHttpClient.Get<UserDTO>("https://localhost:7022/user/username/" + username,true,"Bearer "+response.Token);
                
                user.Token = response.Token;
                user.Username = username;
                user.Role = dto.Role;
                user.TeamId = dto.TeamId;
                user.Id = dto.Id;
                
                SceneManager.LoadScene(1);
            }
            
        }

    }
}
