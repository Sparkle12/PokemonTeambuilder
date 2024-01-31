using Scripts;
using SharedLibrary.DTOs;
using SharedLibrary.Requests;
using SharedLibrary.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TeambuilderUI : MonoBehaviour
{
    private     bool                        initial = true;
    private     TeamDTO                     team;
    private     List<PokemonDTO>            pokemons;
    private     int                         poke1index;
    private     int                         poke2index;
    private     int                         poke3index;
    private     List<MoveDTO>               pokemon1Moves;
    private     List<MoveDTO>               pokemon2Moves;
    private     List<MoveDTO>               pokemon3Moves;
    public      UserInfo                    user;
    public      Image                       pokemon1;
    public      Image                       pokemon2;
    public      Image                       pokemon3;
    public      Dropdown                    pokemon1name;
    public      Dropdown                    pokemon2name;
    public      Dropdown                    pokemon3name;
    public      Dropdown                    pokemon1Move1;
    public      Dropdown                    pokemon1Move2;
    public      Dropdown                    pokemon2Move1;
    public      Dropdown                    pokemon2Move2;
    public      Dropdown                    pokemon3Move1;
    public      Dropdown                    pokemon3Move2;
    public      Text                        poke1Hp;
    public      Text                        poke1Attack;
    public      Text                        poke1SpAttack;
    public      Text                        poke1Defence;
    public      Text                        poke1SpDefence;
    public      Text                        poke1Speed;
    public      Text                        poke2Hp;
    public      Text                        poke2Attack;
    public      Text                        poke2SpAttack;
    public      Text                        poke2Defence;
    public      Text                        poke2SpDefence;
    public      Text                        poke2Speed;
    public      Text                        poke3Hp;
    public      Text                        poke3Attack;
    public      Text                        poke3SpAttack;
    public      Text                        poke3Defence;
    public      Text                        poke3SpDefence;
    public      Text                        poke3Speed;
    public      Text                        pokemon1display;
    public      Text                        pokemon2display;
    public      Text                        pokemon3display;
    public      Text                        pokemon1Type1;
    public      Text                        pokemon1Type2;
    public      Text                        pokemon2Type1;
    public      Text                        pokemon2Type2;
    public      Text                        pokemon3Type1;
    public      Text                        pokemon3Type2;
    public      Text                        maintext;
    
    private async void GetMoves()
    {
        pokemon1Moves = team.Pokemons[0].Learnable;
        pokemon2Moves = team.Pokemons[1].Learnable;
        pokemon3Moves = team.Pokemons[2].Learnable;
    }
    private void LoadSprites()
    {
        Sprite p1 = Resources.Load<Sprite>(team.Pokemons[0].Name);
        Sprite p2 = Resources.Load<Sprite>(team.Pokemons[1].Name);
        Sprite p3 = Resources.Load<Sprite>(team.Pokemons[2].Name);
        pokemon1.GetComponent<Image>().sprite = p1;
        pokemon2.GetComponent<Image>().sprite = p2;
        pokemon3.GetComponent<Image>().sprite = p3;
    }

    private void GetOptions()
    {
        pokemon1name.options.Clear();
        pokemon2name.options.Clear();
        pokemon3name.options.Clear();
        pokemon1Move1.options.Clear();
        pokemon1Move2.options.Clear();
        pokemon2Move1.options.Clear();
        pokemon2Move2.options.Clear();  
        pokemon3Move1.options.Clear();
        pokemon3Move2.options.Clear();

        GetMoves();
        
        var i = 0;
        foreach (var pokemon in pokemons)
        {
            if (pokemon.Name != team.Pokemons[1].Name &&
                pokemon.Name != team.Pokemons[2].Name)
            {
                pokemon1name.options.Add(new Dropdown.OptionData(pokemon.Name));

                if (pokemon.Name == team.Pokemons[0].Name)
                    poke1index = i;
                i++;
            }
        }
        i = 0;
        foreach (var pokemon in pokemons)
        {
            if (pokemon.Name != team.Pokemons[0].Name &&
                pokemon.Name != team.Pokemons[2].Name)
            {
                pokemon2name.options.Add(new Dropdown.OptionData(pokemon.Name));

                if (pokemon.Name == team.Pokemons[1].Name)
                    poke2index = i;
                i++;
            }
        }

        i = 0;
        foreach (var pokemon in pokemons)
        {
            if (pokemon.Name != team.Pokemons[0].Name &&
                pokemon.Name != team.Pokemons[1].Name)
            {
                pokemon3name.options.Add(new Dropdown.OptionData(pokemon.Name));

                if (pokemon.Name == team.Pokemons[2].Name)
                    poke3index = i;
                i++;
            }

        }

        i = 0;
        foreach(var move in pokemon1Moves)
        {
           pokemon1Move1.options.Add(new Dropdown.OptionData(move.Name));
            if (i != pokemon1Move1.value)
                pokemon1Move2.options.Add(new Dropdown.OptionData(move.Name));
            i++;
        }

        i = 0;
        foreach (var move in pokemon2Moves)
        {
            pokemon2Move1.options.Add(new Dropdown.OptionData(move.Name));
            if (i != pokemon2Move1.value)
                pokemon2Move2.options.Add(new Dropdown.OptionData(move.Name));
            i++;
        }

        i = 0;
        foreach (var move in pokemon3Moves)
        {
            pokemon3Move1.options.Add(new Dropdown.OptionData(move.Name));
            if (i != pokemon3Move1.value)
                pokemon3Move2.options.Add(new Dropdown.OptionData(move.Name));
            i++;
        }

        pokemon1name.value = poke1index;
        pokemon2name.value = poke2index;
        pokemon3name.value = poke3index;
    }

    private void SetStats()
    {
        pokemon1Type1.text = team.Pokemons[0].Types[0];
        if (team.Pokemons[0].Types.Count > 1)
            pokemon1Type2.text = team.Pokemons[0].Types[1];
        else
            pokemon1Type2.text = "";

        pokemon1display.text = team.Pokemons[0].Name;
        poke1Hp.text = "Hp: " + team.Pokemons[0].Hp.ToString();
        poke1Attack.text = "Attack: " + team.Pokemons[0].Attack.ToString();
        poke1SpAttack.text = "SpAttack: " + team.Pokemons[0].SpAttack.ToString();
        poke1Defence.text = "Defence: " + team.Pokemons[0].Defence.ToString();
        poke1SpDefence.text = "SpDefence: " + team.Pokemons[0].SpDefence.ToString();
        poke1Speed.text = "Speed: " + team.Pokemons[0].Speed.ToString();

        pokemon2Type1.text = team.Pokemons[1].Types[0];
        if (team.Pokemons[1].Types.Count > 1)
            pokemon2Type2.text = team.Pokemons[1].Types[1];
        else
            pokemon2Type2.text = "";

        pokemon2display.text = team.Pokemons[1].Name;
        poke2Hp.text = "Hp: " + team.Pokemons[1].Hp.ToString();
        poke2Attack.text = "Attack: " + team.Pokemons[1].Attack.ToString();
        poke2SpAttack.text = "SpAttack: " + team.Pokemons[1].SpAttack.ToString();
        poke2Defence.text = "Defence: " + team.Pokemons[1].Defence.ToString();
        poke2SpDefence.text = "SpDefence: " + team.Pokemons[1].SpDefence.ToString();
        poke2Speed.text = "Speed: " + team.Pokemons[1].Speed.ToString();

        pokemon3Type1.text = team.Pokemons[2].Types[0];
        if (team.Pokemons[2].Types.Count > 1)
            pokemon3Type2.text = team.Pokemons[2].Types[1];
        else
            pokemon3Type2.text = "";

        pokemon3display.text = team.Pokemons[2].Name;
        poke3Hp.text = "Hp: " + team.Pokemons[2].Hp.ToString();
        poke3Attack.text = "Attack: " + team.Pokemons[2].Attack.ToString();
        poke3SpAttack.text = "SpAttack: " + team.Pokemons[2].SpAttack.ToString();
        poke3Defence.text = "Defence: " + team.Pokemons[2].Defence.ToString();
        poke3SpDefence.text = "SpDefence: " + team.Pokemons[2].SpDefence.ToString();
        poke3Speed.text = "Speed: " + team.Pokemons[2].Speed.ToString();

    }


    async void Start()
    {
        maintext.text = user.Username + "'s Team";
        pokemons = await MyHttpClient.Get<List<PokemonDTO>>("https://localhost:7022/pokemon/all",true,"Bearer "+user.Token);
        if (user.TeamId == 0)
            user.TeamId = 1;

        team = await MyHttpClient.Get<TeamDTO>("https://localhost:7022/team/" + user.TeamId.ToString(), true, "Bearer " + user.Token);

        LoadSprites();

        GetMoves();

        GetOptions();
        
        SetStats();
                
        initial = false;
    }

    public void OnChange1()
    {
        if (!initial && pokemon1name.value != poke1index)
        {
            Sprite p = Resources.Load<Sprite>(pokemon1name.options[pokemon1name.value].text);
            pokemon1.sprite = p;

            foreach (PokemonDTO pokemon in pokemons)
            {
                if (pokemon.Name == pokemon1name.options[pokemon1name.value].text)
                {
                    team.Pokemons[0] = pokemon;
                    break;
                }
            }

            GetOptions();

            SetStats();
        }
    }

    public void OnChange2()
    {
        if (!initial && pokemon2name.value != poke2index)
        {
            Sprite p = Resources.Load<Sprite>(pokemon2name.options[pokemon2name.value].text);
            pokemon2.sprite = p;

            foreach (PokemonDTO pokemon in pokemons)
            {
                if (pokemon.Name == pokemon2name.options[pokemon2name.value].text)
                {
                    team.Pokemons[1] = pokemon;
                    break;
                }
            }
            
            GetOptions();
            SetStats();
        }
    }

    public void OnChange3()
    {
        if (!initial && pokemon3name.value != poke3index)
        {
            Sprite p = Resources.Load<Sprite>(pokemon3name.options[pokemon3name.value].text);
            pokemon3.sprite = p;

            foreach (PokemonDTO pokemon in pokemons)
            {
                if (pokemon.Name == pokemon3name.options[pokemon3name.value].text)
                {
                    team.Pokemons[2] = pokemon;
                    break;
                }
            }
            
            GetOptions();
            SetStats();
        }
    }

    public void OnChangeMove()
    {
        GetOptions();
    }

    public async void OnClick()
    {
        UpdateTeamRequest req = new UpdateTeamRequest();
        req.PokeId = new List<int>();

        req.UserId = user.Id;

        foreach(PokemonDTO pokemon in team.Pokemons)
        {
            req.PokeId.Add(pokemon.Id);
        }

        var response = await MyHttpClient.Post<UpdateTeamResponse>("https://localhost:7022/user/update/team",req,true,"Bearer " + user.Token);

        Debug.Log(response.TeamId.ToString() + response.Message);
    }

}
