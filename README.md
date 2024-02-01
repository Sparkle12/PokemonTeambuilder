# PokemonTeambuilder
2023-2024 .NET course project. The project is made using unity and a .NET backend

## Login System
The Login System uses hash and salt encryption to keep the password in the database. It also generates JWT tokens that keeps information like user and role Id to make authorization easier( Code found in AuthenticationService and AuthenticationController).

## Pokemons
-Each pokemon has at most 2 types, a list of learnable moves, a set of stats(Hp, Attack, SpAttack, Defence, SpDefence, Speed).

-A type is just string

-Each move contains a name, a type, a set of stats(Power, Accuracy) and a boolean called AttackType that specifies if the move scales of the Attack stat of the pokemon or the SpAttack stat.

-A team contains 3 pokemons. In order to not keep the same set of pokemons for multiple users, all the possible teams are kept in the Teams entity ordered by id.

## Front-End
- The front-end consists in 2 scenes. The first one is the login screen that takes the username and password, checks that they are not null, and makes a authentication request to the authentication controller. If the request is succesful, another request is made to the user controller in order to get the user info. After that the user info is stored into a scriptable object in order to keep the data between scenes 
