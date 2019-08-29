namespace WealthMate.Models
{
    public class TermDeposit
    {
        public string Provider { get; set; }
        public float MinDeposit { get; set; }
        public float MaxDeposit { get; set; }
        public float LengthInYears { get; set; }
        public float InterestRate { get; set; }
        public float InfiniteMaxDeposit; // for term deposits with no max deposit
        public float NoMinimumDeposit; // for term deposits with no min deposit

        public TermDeposit(string provider, float minDeposit, float maxDeposit, float length, float interestRate)
        {
            Provider = provider;
            MinDeposit = minDeposit;
            MaxDeposit = maxDeposit;
            LengthInYears = length;
            InterestRate = interestRate;
        }

        // Done for all but only 1 year length
        // Awaiting group confirmation to do other segments too

  /*      TermDeposit ANZTermDeposit = new TermDeposit("ANZ", 10000.00, null, 1, 2.85);
        TermDeposit ASB1TermInvestment = new TermDeposit("ASB", 5000.00, 9999.00, 1, 2.00);
        TermDeposit ASB2TermInvestment = new TermDeposit("ASB", 10000.00, 49999.00, 1, 2.75);
        TermDeposit BNZ1TermInvestment = new TermDeposit("BNZ", 2000.00, 4999.00, 1, 2.80);
        TermDeposit BNZ1TermInvestment = new TermDeposit("BNZ", 5000.00, 5000000.00, 1, 2.80);
        TermDeposit AssetFinanceTermDeposit = new TermDeposit("Asset Finance", 500, InfiniteMaxDeposit, 1, 3.60);
        TermDeposit ChinaConstructionBankTermDeposit = new TermDeposit("China Construction Bank", 100000.00, 5000000.00, 1, 2.70);
        TermDeposit CooperativeBank1TermDeposit = new TermDeposit("Co-operative Bank", 2000.00, 9999.00, 1, 2.60); // changed in the last 7 days
        TermDeposit CooperativeBank2TermDeposit2 = new TermDeposit("Co-operative Bank", 10000.00, 49999.00, 1, 2.70); // changed in the last 7 days
        TermDeposit CooperativeBank3TermDeposit3 = new TermDeposit("Co-operative Bank", 50000.00, InfiniteMaxDeposit, 1, 2.70); // changed in the last 7 days
        TermDeposit CooperativeBankSpecialTermDeposit = new TermDeposit("Co-operative Bank", 5000.00, InfiniteMaxDeposit, 1, 2.75); // changed in the last 7 days
        TermDeposit FEInvestmentsTermDeposit = new TermDeposit("FE Investments", 5000.00, InfiniteMaxDeposit, 1, 4.40);
        TermDeposit FirstCreditUnion1TermDeposit = new TermDeposit("First Credit Union", 500.00, 9999.00, 1, 3.50);
        TermDeposit FirstCreditUnion2TermDeposit = new TermDeposit("First Credit Union", 10000.00, 49999.00, 1, 3.50);
        TermDeposit FirstCreditUnion3TermDeposit = new TermDeposit("First Credit Union", 50000.00, InfiniteMaxDeposit, 1, 3.50);
        TermDeposit GeneralFinanceDepositRate = new TermDeposit("General Finance", 5000.00, 100000.00, 1, 4.65);
        TermDeposit HeartlandBankTermDeposit = new TermDeposit("Heartland Bank", 1000.00, InfiniteMaxDeposit, 1, 3.05);
        TermDeposit HeretaungaBuildingSocietyTermInvestment = new TermDeposit("Heretaunga Building Society", 1.00, InfiniteMaxDeposit, 3.60);
        TermDeposit HSBCPremier1TermDeposit = new TermDeposit("HSBC Premier", NoMinimumDeposit, 9999.00, 1, 1.20); // changed in the last 7 days 24/08/2019
        TermDeposit HSBCPremier2TermDeposit = new TermDeposit("HSBC Premier", 10000.00, 99999.00, 1, 2.20); // changed in the last 7 days 24/08/2019 
        TermDeposit HSBCPremier3TermDeposit = new TermDeposit("HSBC Premier", 100000.00, InfiniteMaxDeposit, 1, 2.40); // changed in the last 7 days 24/08/2019
        TermDeposit Kiwibank1TermDeposit = new TermDeposit("Kiwibank", 1000.00, 4999.00, 1, 2.00);
        TermDeposit Kiwibank2TermDeposit = new TermDeposit("Kiwibank", 5000.00, 9999.00, 1, 2.65);
        TermDeposit Kiwibank3TermDeposit = new TermDeposit("Kiwibank", 10000.00, 49999.00, 1, 2.75);
        TermDeposit Kiwibank4TermDeposit = new TermDeposit("Kiwibank", 50000.00, 5000000.00, 1, 2.75);
        TermDeposit KookminNZ4TermDeposit = new TermDeposit("Kookmin - NZ", 5000.00, 9999.00, 1, 3.20);
        TermDeposit KookminNZ1TermDeposit = new TermDeposit("Kookmin - NZ", 10000.00, 49999.00, 1, 3.30);
        TermDeposit KookminNZ2TermDeposit = new TermDeposit("Kookmin - NZ", 50000.00, 99999.00, 1, 3.40);
        TermDeposit KookminNZ3TermDeposit = new TermDeposit("Kookmin - NZ", 100000.00, InfiniteMaxDeposit, 1, 3.50);
        TermDeposit Liberty1TermDeposit = new TermDeposit("Liberty", 5000.00, 19999.00, 1, 3.60);
        TermDeposit Liberty2TermDeposit = new TermDeposit("Liberty", 20000.00, 99999.00, 1, 3.85);
        TermDeposit Liberty3TermDeposit = new TermDeposit("Liberty", 100000.00, InfiniteMaxDeposit, 1, 4.25);
        TermDeposit NapierBuildingSocietyTermDeposit = new TermDeposit("Napier Building Society", 5000.00, InfiniteMaxDeposit, 1, null);
        TermDeposit NelsonBuildingSocietyTermDeposit = new TermDeposit("Nelson Building Society", 5000.00, 250000, 1, 3.20);
        TermDeposit NZCUAuckland1TermDeposit = new TermDeposit("NZCU Auckland", 500.00, 9999.00, 1, 2.90); // changed in the last 7 days 24/08/2019
        TermDeposit NZCUAuckland2TermDeposit = new TermDeposit("NZCU Auckland", 10000.00, 500000, 1, 3.10); // changed in the last 7 days 24/08/2019
        TermDeposit NZCUBaywideTermDeposit = new TermDeposit("NZCU Baywide", 1000.00, 1000000.00, 1, 3.25);
        TermDeposit NZCUSouth1TermDeposit = new TermDeposit("NZCU South", 500.00, 4999.00, 1, 2.25);
        TermDeposit NZCUSouth2TermDeposit = new TermDeposit("NZCU South", 5000.00, 500000.00, 1, 3.25);
        TermDeposit PublicTrust1TermDeposit = new TermDeposit("Public Trust", 5000.00, 9999.00, 1, 1.00);
        TermDeposit PublicTrust2TermDeposit = new TermDeposit("Public Trust", 10000.00, 249999.00, 1, 1.00);
        TermDeposit PublicTrust3TermDeposit = new TermDeposit("Public Trust", 50000.00, 9999.00, 1, 1.00);
        TermDeposit PublicTrust4TermDeposit = new TermDeposit("Public Trust", 250000.00, InfiniteMaxDeposit, 1, 1.00);
        TermDeposit RabobankTermDeposit = new TermDeposit("Rabobank", 1000.00, InfiniteMaxDeposit, 1, 2.95);
        TermDeposit SBSBankTermInvestmentSpecial = new TermDeposit("SBS Bank", 1000.00, 250000.00, 1, 2.75); // changed in the last 7 days 24/08/2019
        TermDeposit TSBBank1TermDeposit = new TermDeposit("TSB Bank", 5000.00, 9999.00, 1, 2.65);
        TermDeposit TSBBank2TermDeposit = new TermDeposit("TSB Bank", 10000.00, 25000.00, 1, 2.75);
        TermDeposit WairarapaBldgSocy1TermInvestment = new TermDeposit("Wairarapa Bldg Socy", 500.00, 1999.00, 1, 1.50);
        TermDeposit WairarapaBldgSocy2TermInvestment = new TermDeposit("Wairarapa Bldg Socy", 2000.00, 4999.00, 1, 2.30);
        TermDeposit WairarapaBldgSocy3TermInvestment = new TermDeposit("Wairarapa Bldg Socy", 5000.00, InfiniteMaxDeposit, 1, 3.10);
        TermDeposit WestpacTermInvestment = new TermDeposit("Westpac", 5000.00, 5000000.00, 1, 2.75);*/


    }
}
