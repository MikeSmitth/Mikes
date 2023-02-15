INCLUDE Globals.ink

//IF to jest if... chyba 
//trzeba ustawić od jakiej ""funkcji zaczynamy

VAR indx=0
->Dialogues

=== Dialogues ===
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

{Bob:
-0:

->FirstMeetingBobMike 





-else:
        {indx==40:
         BYE Obeszliśmy całą tablice#speaker:ERROR #layout:right
         ->END
        -else:
         ~indx=indx+1
         {CheckBobDialogueLine(indx,1)}
         }

}


//BOB


===  FirstMeetingBobMike ===
#speaker:Bob #portrait:Oswald_Neutral #layout:right 
{CheckBobDialogueLine(2,1)}
{CheckBobDialogueLine(9,2)}
//{CheckDialogueLine(bob,11,3)} zmieniony jest w dialogu
{CheckBobDialogueLine(10,4)}

{
-dialogueline1==0: 
{ChangeMikeDialogueLine(2)}
{ChangeBobDialogueLine(2)}
//~changeDialogueLine = "false"

#observation:The first meeting with men-1 #observation:The first meeting with men-2 #observation:The first meeting with men-3 #observation:The first meeting with men-4 
#observation:Camp in the mountains-1 #observation:Camp in the mountains-2 #observation:Camp in the mountains-3 #observation:Camp in the mountains-4 #observation:Camp in the mountains-5 #observation:Camp in the mountains-9

Krótkie przywitanie i przedstawienie się.  
Przedstawiasz im się prawdziwym imieniem,  
raczej mało prawdopodobne, by Cię znali i twoje przewinienia. 
Za to Oni przedstawiają się z nieprawdziwego imienia ukrywają tożsamość 

->FirstMeetingBobMikeDialogue

-dialogueline4==10: 
 Szerokiej Drogi
->END


-else:
Chyba Ci się nie śpieszy. #speaker:Mike #portrait:Edwin_Smile #layout:left
Rozbijaj bo zamarzniesz. 
Zaraz pomożemy, tylko chrustu na kolacje przygotujemy. #speaker:Bob #portrait:Oswald_Neutral #layout:right
->END

 }
 
=== FirstMeetingBobMikeDialogue ===

 *{dialogueline2==9} [1Możesz dopytać się czy są spokrewnieni] Poznali się  pare dni temu na szlaku. 
 Innymi słowy nie powinni się oni dobrze znać, może przyda się to w przyszłości. 
 ->FirstMeetingBobMikeDialogue
  
  *{dialogueline3==11}  [3Dopytujesz skąd nowi znajomi są] Edwin wspomniał,
  że rodzinne sprawy pociągają go do szybkiego powrotu, do rodzinnego miasta
  Oswald Wymijająco odpowiedział,że śpieszy mu się do Lamberku #speaker:Mike #portrait:Edwin_Neutral #layout:left
  ->FirstMeetingBobMikeDialogue

*[2Pytasz się dokąd zmierzają]
Odpowiadają, że  w tą samą stronę. 
Pytają skąd ty jesteś i dokąd zmierzasz.
{ChangeBobDialogueLine(11)}
{ChangeMikeDialogueLine(11)}
~dialogueline3 = 11
    **[Mówisz skąd pochodzisz, Pomijasz incydent w Tavernie]
{ChangeBobDialogueLine(4)}
{ChangeMikeDialogueLine(4)}
    ->FirstMeetingBobMikeDialogue
    **[Mówisz skąd pochodzisz. Opowiadasz, że byłeś w Tavernie]
{ChangeBobDialogueLine(5)}
{ChangeMikeDialogueLine(5)}
    ->FirstMeetingBobMikeDialogue
    **[Mówisz skąd pochodzisz.Opowiadasz, że byłeś w Tavernie, ogólnikowo opisujesz incydent, pomijasz aspekt nagrody za Ciebie]
{ChangeBobDialogueLine(6)}
{ChangeMikeDialogueLine(6)}
    ->FirstMeetingBobMikeDialogue
    

*[4Pytasz czy możesz dołączyć do obozowiska]
Odpowiadają, że nie ma problemu i zapraszaja obok do rozbicia się. Jest już trochę zdarneitęgo sniegu po ich tobołach. 
{ChangeBobDialogueLine(7)}
{ChangeMikeDialogueLine(7)}
->END

*[5Jedziesz dalej sam, życzysz udanej podróży]
Wzajemnie #speaker:Mike #portrait:Edwin_Smile #layout:left
{ChangeBobDialogueLine(10)}
{ChangeMikeDialogueLine(10)}
->END



//MIKE
=== MikeStart ===
Hi? #time:1 #speaker:Mike #portrait:Edwin_Neutral #layout:right
->END
   