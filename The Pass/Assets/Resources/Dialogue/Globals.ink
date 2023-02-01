
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


VAR Bob = 0
VAR Mike = 0
VAR john = 0



VAR dialogueline1 = 0
VAR dialogueline2 = 0
VAR dialogueline3 = 0
VAR dialogueline4 = 0
VAR indexBufor = 0

=== function CheckMikeDialogueLine(index,line) ===

~changeDialogueLine = "false"

~Mike = index 

#skip:BUFOR1
BUFOR1 
#skip:BUFOR2
BUFOR2 

{dialogueLineEdit(Mike,line)}



//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta

=== function CheckBobDialogueLine(index,line) ===

~changeDialogueLine = "false"

~Bob = index 

#skip:BUFOR1
BUFOR1 
#skip:BUFOR2
BUFOR2 

{dialogueLineEdit(Bob,line)}



//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta
=== function dialogueLineEdit(index,line) ===

{line:

-1: 
~dialogueline1 = index

-2: 
~dialogueline2 = index

-3:
~dialogueline3 = index

-4: 
~dialogueline4 = index

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ dialogueline #skip:bufor
}









=== function CheckDialogueLine(name,index,line) ===

~changeDialogueLine = "false"

{name:
-Bob: 
~Bob = index 

-Mike: 
~Mike = index 

-john:
~john = index 

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ name #skip:bufor
}


#skip:BUFOR1
BUFOR1 
#skip:BUFOR2
BUFOR2 




{name:
-Bob: 
~indexBufor = Bob 

-Mike: 
~indexBufor = Mike

-john:
~indexBufor = john 

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ name #skip:bufor
}




{line:

-1: 
~dialogueline1 = indexBufor

-2: 
~dialogueline2 = indexBufor

-3:
~dialogueline3 = indexBufor

-4: 
~dialogueline4 = indexBufor

-else:
NIC SIĘ NIE STAŁO, ZŁY NR ZNIENNEJ dialogueline #skip:bufor
}



//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta









=== function ChangeBobDialogueLine(index) ===

~changeDialogueLine = "true"
~Bob = index

//BOB
=== function ChangeMikeDialogueLine(index) ===

~changeDialogueLine = "true"
~Mike = index

//Mike

