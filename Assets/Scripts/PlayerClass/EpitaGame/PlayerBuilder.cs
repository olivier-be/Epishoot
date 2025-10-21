using PlayerClass.EpitaGame.Models;
using Character = PlayerClass.EpitaGame.Models.Character;

namespace EpitaGame
{
    public enum TypePlayer
    {
        ACU,
        YAKA,
        APPING,
        ING,
    }
    public class PlayerBuilder
    {
        public static Character Spawn(TypePlayer typePlayer)
        {
            if (typePlayer == TypePlayer.ACU)
            {
                return new AssistantACU("");
            }
            else if (typePlayer == TypePlayer.APPING)
            {
                return new StudentAPPING("");

            }
            else if (typePlayer == TypePlayer.ING)
            {
                return new StudentING("");

            }
            else if (typePlayer == TypePlayer.YAKA)
            {
                return new AssistantYAKA("");

            }
            return null;
        }
    }
}