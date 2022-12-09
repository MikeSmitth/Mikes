INCLUDE Globals.ink

//IF to jest if... chyba 
//trzeba ustawić od jakiej ""funkcji zaczynamy

VAR indx=0
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
=== function ChangeBobDialogueLine(index) ===

~changeDialogueLine = "true"
~bob = index
BUFOR #skip:bufor
zmiana {bob} od bob dla index {index} #skip:bufor
//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta

//~return bob


//BOB
=== function CheckBobDialogueLine(index) ===

~changeDialogueLine = "false"
~bob = index 

BUFOR  #skip:bufor
sprawdzenie {bob} od bob dla index {index} #skip:bufor 


//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta
~return bob

//Mike
=== function ChangeMikeDialogueLine(index) ===

~changeDialogueLine = "true"
~mike = index

BUFOR #skip:bufor
zmiana {mike} od mike dla index {index}#skip:bufor
//Bufor by INK mógł zareagować na zmiane zminnej gdy "changeDialogueLine" jest "false", a sama amienna nie  została odkryta



//Mike
=== function CheckMikeDialogueLine(index) ===

~changeDialogueLine = "false"
~mike = index
BUFOR #skip:bufor
sprawdzenie {mike} od mike dla index {index}#skip:bufor

~return mike


//BOB
=== BobStart ===

{bob:
-0:
->FirstMeeting





-else:
        {indx==40:
         BYE Obeszliśmy całą tablice#speaker:ERROR #layout:right
         ->END
        -else:
         ~indx=indx+1
         {CheckBobDialogueLine(indx)}
         }

}


//BOB
=== FirstMeeting ===
{ChangeBobDialogueLine(2)}
{ChangeMikeDialogueLine(2)}


krótkie przywitanie i przedstawienie się.  #speaker:Bob #portrait:neutral #layout:right
Przedstawiasz im się prawdziwym imieniem,  
raczej mało prawdopodobne, by Cię znali   
i twoje przewinienia. Za to Oni przedstawiają się  

 {CheckBobDialogueLine(9)==9: 
 
 *[1Możesz dopytać się czy są spokrewnieni]
        Poznali się  pare dni temu na szlaku. 
        Innymi słowy nie powinni się oni dobrze znać, może przyda się to w przyszłości. 
        ->FirstMeeting
  }
  
  {CheckBobDialogueLine(11)==11:  

*[3Dopytujesz skąd nowi znajomi są]
Edwin wspomniał, że rodzinne sprawy pociągają go do szybkiego powrotu, do rodzinnego miasta
Oswald Wymijająco odpowiedział,że śpieszy mu się do Lamberku #speaker:Mike #portrait:neutral #layout:left
->FirstMeeting    
 }
 
 
 Z nieprawdziwego imienia ukrywają tożsamość #time:1 

*[2Pytasz się dokąd zmierzają, Odpowiadają, że  w tą samą stronę]
{ChangeBobDialogueLine(11)}
{ChangeMikeDialogueLine(11)}
Pytają skąd ty jesteś i dokąd zmierzasz 
    **[Mówisz skąd pochodzisz, Pomijasz incydent w Tavernie]
{ChangeBobDialogueLine(4)}
{ChangeMikeDialogueLine(4)}
    ->FirstMeeting
    **[Mówisz skąd pochodzisz. Opowiadasz, że byłeś w Tavernie]
{ChangeBobDialogueLine(5)}
{ChangeMikeDialogueLine(5)}
    ->FirstMeeting
    **[Mówisz skąd pochodzisz.Opowiadasz, że byłeś w Tavernie, ogólnikowo opisujesz incydent, pomijasz aspekt nagrody za Ciebie]
{ChangeBobDialogueLine(6)}
{ChangeMikeDialogueLine(6)}
    ->FirstMeeting
    

*[4Jecie razem  Idziecie spać]
aaa 
->FirstMeeting

*[5Jedziesz dalej sam, życzysz udanej podróży]
czx 
->FirstMeeting








->END



 




//MIKE
=== MikeStart ===
Hi? #time:1 #speaker:Mike #portrait:neutral #layout:right
->END
   