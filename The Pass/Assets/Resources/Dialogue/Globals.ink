
VAR pokemon_name = ""


VAR speaker = ""
VAR time = 0


//ZMIENE do DIALOGUETIMELINE

//changeDialogueLine ZAWSZE MUSI BYĆ W DIALOGU PRZED 
// ZMIENNĄ IMMIENNĄ DIALOGUETIMELINEm, 
//poneważ ona jest pobarana i sprawdzana, 
// by wiedzieć co zrobić dalej. 
//dodając postacie dodaj też zmienną w 
//dialogue menager pod tym samym umieniem
VAR changeDialogueLine = "false"


VAR bob = 0
VAR mike = 0
VAR john = 0



VAR dialogueline1 = 0
VAR dialogueline2 = 0
VAR dialogueline3 = 0
VAR dialogueline4 = 0


=== function CheckDialogueLine(name,index,line) ===
~changeDialogueLine = "false"

{name:
-bob: 
~bob = index 

-mike: 
~mike = index 

-john:
~john = index 

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ name #skip:bufor
}


#skip:BUFOR1
BUFOR1 
#skip:BUFOR2
BUFOR2 

{line:

-1: 
~dialogueline1 = bob

-2: 
~dialogueline2 = bob

-3:
~dialogueline3 = bob

-4: 
~dialogueline4 = bob

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ dialogueline #skip:bufor
}



//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta

=== function ChangeBobDialogueLine(index) ===

~changeDialogueLine = "true"
~bob = index

//BOB
=== function ChangeMikeDialogueLine(index) ===

~changeDialogueLine = "true"
~mike = index

//Mike

