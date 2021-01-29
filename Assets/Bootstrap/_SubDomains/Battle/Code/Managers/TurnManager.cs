using System;
using Bootstrap._SubDomains.Battle.Code.Settings;

namespace Bootstrap._SubDomains.Battle.Code.Managers
{
    public class TurnManager
    {
        public int CurrentPlayerId { get; private set; }
        public int CurrentRoundNumber { get; private set; }

        public event Action RoundStarted;
        
        private readonly LevelConfig _config;

        public TurnManager(LevelConfig config)
        {
            _config = config;
            CurrentPlayerId = -1;
            CurrentRoundNumber = 0;
        }

        public void StartRound()
        {
            CurrentPlayerId = 0;
            CurrentRoundNumber++;
            RoundStarted?.Invoke();
        }

        public void TurnEnded()
        {
            CurrentPlayerId++;
            if (CurrentPlayerId >= _config.Players.Length)
            {
                StartRound();
            }
        }
    }
}