using System;
using PlayerClass.EpitaGame.Models;
using CombatGame = PlayerClass.EpitaGame.Game.CombatGame;

namespace EpitaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║  🎮 SYSTÈME DE COMBAT EPITA - Tests      ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            Console.WriteLine("Choisissez le mode de test :");
            Console.WriteLine("1. Combat standard");
            Console.WriteLine("2. Tester tous les types de personnages");
            Console.Write("\nVotre choix : ");
            
            string choice = Console.ReadLine();

            if (choice == "2")
            {
                TestAllCharacters();
            }
            else
            {
                CombatGame game = new CombatGame();
                game.StartGame();
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }

        static void TestAllCharacters()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("TEST DE TOUS LES TYPES DE PERSONNAGES");
            Console.WriteLine(new string('=', 50) + "\n");

            IPlayer apping = new StudentAPPING("Bob_APPING");
            IPlayer ing = new StudentING("Alice_ING");
            IPlayer yaka = new AssistantYAKA("Prof_YAKA");
            IPlayer acu = new AssistantACU("Prof_ACU");

            Console.WriteLine(" Statistiques des personnages :\n");
            apping.DisplayStatus();
            ing.DisplayStatus();
            yaka.DisplayStatus();
            acu.DisplayStatus();

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("TEST 1 : Compétence spéciale StudentAPPING (Double dégâts)");
            Console.WriteLine(new string('-', 50) + "\n");
            
            ((StudentAPPING)apping).SpecialSkill(yaka);

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("TEST 2 : Bouclier StudentING (Soin)");
            Console.WriteLine(new string('-', 50) + "\n");
            
            ing.TakeDamage(30);
            ((StudentING)ing).Shield();

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("TEST 3 : Tir rapide AssistantYAKA");
            Console.WriteLine(new string('-', 50) + "\n");
            
            ((AssistantYAKA)yaka).RapidFire(apping);

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("TEST 4 : Coup critique AssistantACU");
            Console.WriteLine(new string('-', 50) + "\n");
            
            ((AssistantACU)acu).CriticalStrike(ing);

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STATUT FINAL");
            Console.WriteLine(new string('=', 50) + "\n");
            
            apping.DisplayStatus();
            ing.DisplayStatus();
            yaka.DisplayStatus();
            acu.DisplayStatus();

            Console.WriteLine("\n Tous les types de personnages ont été testés avec succès !");
        }
    }
}