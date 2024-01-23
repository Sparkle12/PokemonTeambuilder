using Scripts;
using SharedLibrary;
using System;
using System.Net.Http;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var player = await MyHttpClient.Get<Player>("https://localhost:7022/player/100/ana");
        Debug.Log(player.Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
