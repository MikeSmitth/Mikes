INCLUDE Globals.ink

//IF to jest if... chyba 
//trzeba ustawić od jakiej ""funkcji zaczynamy
->main

=== main ===
{speaker:

-"Mike": 
->MikeStart

-"Bob":
->BobStart

-else:
???NoneSpekaer 
}
->END



//BOB
=== BobStart ===
{ pokemon_name != "": ->chosen(pokemon_name)}

~changeDialogueLine = "false"
~bob = 2
sBufor
Which pokemon do you choose? {bob == 2: Yep  | Nop  }#time:1 #speaker:Bob #portrait:neutral #layout:right



~changeDialogueLine = "false"
~bob = 2
Dialog/ ta linika i tak będzie pominięta, jest po to by w następnej można było już wyświetlić zmieniony wynik bob 2 który będzie wynosił 0


Which pokemon do you choose? {bob == 2: Yep  | Nop  }#time:1 #speaker:Bob #portrait:neutral #layout:right

    + [Charmander]
    #time:2
        -> chosen("Charmander") 
    + [Bulbasaur]
    #time:2
        -> chosen("Bulbasaur")
    + [Squirtle]
    #time:2
        -> chosen("Squirtle")
    + [Kurwa Jebana]
    #time:2
        -> chosen("Kurwa Jebana") 
    
=== chosen(pokemon) ===


~pokemon_name=pokemon
{pokemon_name:

-"Kurwa Jebana": 
You chose {pokemon_name}!  #speaker:Bob #portrait:mad

-"Squirtle":
You chose {pokemon_name}!  #speaker:Bob #portrait:neutral

-else:
You chose {pokemon_name}!  #speaker:Bob #portrait:smile
}
WOW! #time:1 #observation:V1Print-1 #portrait:smile #speaker:Mike #layout:left

~changeDialogueLine = "false"
~bob = 2
sBufor
 {bob == 2: Yep  | Nop  }#time:1 #speaker:Bob #portrait:neutral #layout:right
-> END



//MIKE
=== MikeStart ===
Hi? #time:1 #speaker:Mike #portrait:neutral #layout:right
->END
   