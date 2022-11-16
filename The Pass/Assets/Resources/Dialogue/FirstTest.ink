
-> main

=== main ===
Which pokemon do you choose? #speaker:Bob #portrait:neutral #layout:right
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
    + [Kurwa Jebana]
        -> chosen("Kurwa Jebana") 
    
=== chosen(pokemon) ===

{pokemon:

-"Kurwa Jebana": 
You chose {pokemon}! #portrait:mad

-"Squirtle":
You chose {pokemon}! #portrait:neutral

-else:
You chose {pokemon}! #portrait:smile
}


Hi #portrait:smile #speaker:Mike #layout:left
-> END