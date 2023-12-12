using System;

public class OnMoneyProvided : EventArgs
{
   public int AmountOfProvidedMoney;

   public OnMoneyProvided(int amountOfProvidedMoney)
   {
      AmountOfProvidedMoney = amountOfProvidedMoney;
   }
}
