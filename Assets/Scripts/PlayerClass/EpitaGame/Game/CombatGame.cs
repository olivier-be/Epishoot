using System;
using PlayerClass.EpitaGame.Models;

namespace PlayerClass.EpitaGame.Game
{
    public class CombatGame
    {
        private IPlayer student;
        private IPlayer assistant;

        public void StartGame()
        {
            DisplayTitle();
            InitializeCharacters();
            PlayMatch();
        }

        private void InitializeCharacters()
        {
            student = new StudentAPPING("APPING1");
            assistant = new AssistantACU("ACU");

            Console.WriteLine(" Personnages créés :");
            student.DisplayStatus();
            assistant.DisplayStatus();
            Console.WriteLine();
        }

        private void PlayMatch()
        {
            Console.WriteLine("⚔️  DÉBUT DU COMBAT !\n");

            int round = 1;

            while (student.IsAlive && assistant.IsAlive)
            {
                Console.WriteLine($"--- Tour {round} ---");
                
                student.Attack(assistant);
                
                if (!assistant.IsAlive)
                {
                    break;
                }
                
                assistant.Attack(student);
                
                Console.WriteLine();
                round++;
            }

            DisplayWinner();
            AskToReplay();
        }

        private void DisplayWinner()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("🏆 FIN DU JEU 🏆");
            Console.WriteLine(new string('=', 50));
            
            student.DisplayStatus();
            assistant.DisplayStatus();
            
            Console.WriteLine();

            if (student.IsAlive)
            {
                Console.WriteLine($" {student.Name} A GAGNÉ !");
            }
            else if (assistant.IsAlive)
            {
                Console.WriteLine($" {assistant.Name} A GAGNÉ !");
            }
            else
            {
                Console.WriteLine("⚔️  Match nul ! Les deux sont morts !");
            }

            Console.WriteLine(new string('=', 50) + "\n");
        }

        private void AskToReplay()
        {
            Console.WriteLine("Voulez-vous rejouer ? (O/N)");
            string response = Console.ReadLine()?.ToUpper();

            if (response == "O" || response == "OUI")
            {
                Console.WriteLine("\n Nouvelle partie...\n");
                StartGame();
            }
            else
            {
                Console.WriteLine("\n Merci d'avoir joué ! Au revoir !");
            }
        }

        private void DisplayTitle()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║   🎮 JEU DE COMBAT EPITA - Version 1.0   ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");
        }
    }
}
