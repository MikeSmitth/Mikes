INCLUDE Globals.ink

//IF to jest if... chyba 
//trzeba ustawić od jakiej ""funkcji zaczynamy

->main
=== main ===
{speaker:

-"FirstCamp": 
->FirstCamp


-else:
???NoneMonologue
}
->END

=== FirstCamp ===
{CheckBobDialogueLine(7,1)}
#speaker:Protagonist #portrait:neutral #layout:left
Odgarnięty śnieg, pewnie tu leżały wcześniej ich toboły 


{dialogueline1==7:
Chyba to tu miałem się rozbić
*[Rozbij się] Rozbijasz sie w spokoju
->END

}


  *[Odejdź]Dajesz sobie jeszcze chwilkę by rozejrzeć się po okolicy 
  ->END



->END

   