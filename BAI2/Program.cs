using System;
using System.Collections.Generic;


namespace BAI
{
    public partial class BAI_Afteken2
    {
        public static bool Vooruit(uint b)
        {
            //bit 7 geeft aan of de trein vooruit rijdt of niet.
            //b wordt omgezet naar binair getal en dan wordt er gekeken naar de 7de bit.
            // wanneer deze niet 0 is dan rijdt de trein vooruit 
            return (b & (1 << 7)) != 0;
        }
        public static uint Vermogen(uint b)
        {
            // Bits 5 en 6 geven het vermogen aan.
            // Verschuiven bits 5 en 6 naar rechts (b >> 5) en daarna maskeren met 0b11 om alleen deze bits te bekijken.
            uint vermogenBits = (b >> 5) & 0b11;

            // Aan de hand van de bits wordt het vermogen aangegeven.
            switch (vermogenBits)
            {
                case 0b00:
                    return 0;   // 0% vermogen
                case 0b01:
                    return 33;  // 33% vermogen
                case 0b10:
                    return 67;  // 67% vermogen
                case 0b11:
                    return 100; // 100% vermogen
                default:
                    return 0;   // Veiligheidsmaatregel
            }
        }
        public static bool Wagon(uint b)
        {
            // bit 4 geeft aan of er een wagon aan hangt
            // b wordt omgezet naar binair getal en dan wordt er gekeken naar de 4de bit.
            // wanneer deze niet 0 is dan hangt er een wagon aan

            return (b & (1 << 4)) != 0;
           
        }
        public static bool Licht(uint b)
        {
            // bit 3 geeft aan of het licht aan is
            // b wordt omgezet naar binair getal en dan wordt er gekeken naar de 3de bit.
            // wanneer deze niet 0 is dan is het licht aan
            return (b & (1 << 3)) != 0;
        }
        public static uint ID(uint b)
        {
            // bits 0, 1 en 2 geven het ID aan.
            // Maskeren met 0b111 om alleen deze bits te bekijken.
            uint idBits = b & 0b111;

            return idBits;
        }

        public static HashSet<uint> Alle(List<uint> inputStroom)
        {
            // Hashset zorgt ervoor dat er geen dubbele waardes in de set komen
            // De inputstroom waar alle treinen in staan wordt in de hashset gezet
            HashSet<uint> set = new HashSet<uint>(inputStroom);
          
            return set;
        }
        public static HashSet<uint> ZonderLicht(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>();
            //inputstroom wordt doorlopen dmv indexen en als het licht niet aan is
            //wordt het item toegevoegd aan de hashset
            for (int i = 0; i < inputStroom.Count; i++)
            {
                // Als het licht niet aan is, voeg het item toe aan de HashSet
                if (!Licht(inputStroom[i]))
                {
                    set.Add(inputStroom[i]);
                }
            }
            return set;
        }
        public static HashSet<uint> MetWagon(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>();
            //inputstroom wordt doorlopen dmv indexen en als er een wagon aan hangt wordt het item toegevoegd aan de hashset

            for (int i = 0; i < inputStroom.Count; i++)
            {
                // Als er een wagon aan hangt wordt het item toegevoegd aan de HashSet
                if (Wagon(inputStroom[i]))
                {
                    set.Add(inputStroom[i]);
                }
            }
            return set;
        }
        public static HashSet<uint> SelecteerID(List<uint> inputStroom, uint lower, uint upper)
        {
            HashSet<uint> set = new HashSet<uint>();
            //inputstroom wordt doorlopen dmv indexen en als het ID tussen de lower en upper grens ligt
            for (int i = 0; i < inputStroom.Count; i++)
            {
                // Als het ID tussen de lower en upper grens ligt, voeg het item toe aan de HashSet
                if (ID(inputStroom[i]) >= lower && ID(inputStroom[i]) <= upper)
                {
                    set.Add(inputStroom[i]);
                }
            }
            return set;
        }

        public static HashSet<uint> Opdr3a(List<uint> inputStroom)
        {
            // maakt hashset op basis van de functie selecteerID er worden 3 parameters meegegeven
            // de inputstroom dat zijn all treinen, 0 en 2 zijn de lower en upper grens 
            HashSet<uint> treinIdLagerDanDrie = SelecteerID(inputStroom, 0, 2);

            // maakt hashset op basis van de functie ZonderLicht er wordt 1 parameter meegegeven
            // de inputstroom wordt gefilterd door de functie ZonderLicht en worden in de hashset gezet
            HashSet<uint> treinZonderLicht = ZonderLicht(inputStroom);

            // vergelijkt de twee hashsets en haalt de overeenkomsten eruit
            treinIdLagerDanDrie.IntersectWith(treinZonderLicht);

            return treinIdLagerDanDrie;
        }

        public static HashSet<uint> Opdr3b(List<uint> inputStroom)
        {
            //Haalt alle treinen (uit opdracht 2)
            HashSet<uint> alleTreinen = Alle(inputStroom);
          
            //Haalt treinen met ID lager dan 3 (uit opdracht 3a)
            HashSet<uint> treinenMetIDLagerDan3 = Opdr3a(inputStroom);
            //niewuwe hashset voor de resultaten
            HashSet<uint> resultSet = new HashSet<uint>(alleTreinen);

            //Maakt een exceptWith door de treinen met ID lager dan 3 te verwijderen uit de resultSet
            resultSet.ExceptWith(treinenMetIDLagerDan3);

            return resultSet;
        }

        public static void ToonInfo(uint b)
        {
            Console.WriteLine($"ID {ID(b)}, Licht {Licht(b)}, Wagon {Wagon(b)}, Vermogen {Vermogen(b)}, Vooruit {Vooruit(b)}");
        }

        public static List<uint> GetInputStroom()
        {
            List<uint> inputStream = new List<uint>();
            for (uint i = 0; i < 256; i++)
            {
                inputStream.Add(i);
            }
            return inputStream;
        }

        public static void PrintSet(HashSet<uint> x)
        {
            Console.Write("{");
            foreach (uint i in x)
                Console.Write($" {i}");
            Console.WriteLine($" }} ({x.Count} elementen)");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("=== Opgave 1 ===");
            ToonInfo(210);
            Console.WriteLine();

            List<uint> inputStroom = GetInputStroom();

            Console.WriteLine("=== Opgave 2 ===");
            HashSet<uint> alle = Alle(inputStroom);
            PrintSet(alle);
            HashSet<uint> zonderLicht = ZonderLicht(inputStroom);
            PrintSet(zonderLicht);
            HashSet<uint> metWagon = MetWagon(inputStroom);
            PrintSet(metWagon);
            HashSet<uint> groter6 = SelecteerID(inputStroom, 6, 7);
            PrintSet(groter6);
            Console.WriteLine();

            Console.WriteLine("=== Opgave 3a ===");
            HashSet<uint> opg3a = Opdr3a(inputStroom);
            PrintSet(opg3a);
            foreach (uint b in opg3a)
            {
                ToonInfo(b);
            }
            Console.WriteLine();

            Console.WriteLine("=== Opgave 3b ===");
            HashSet<uint> opg3b = Opdr3b(inputStroom);
            PrintSet(opg3b);
            foreach (uint b in opg3b)
            {
                ToonInfo(b);
            }
            Console.WriteLine();
        }
    }
}
