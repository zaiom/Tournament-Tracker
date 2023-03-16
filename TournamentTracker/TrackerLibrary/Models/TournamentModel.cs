using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represent model of tournament.
    /// </summary>
    public class TournamentModel
    {
        public event EventHandler<DateTime> OnTournamentComplete;

        /// <summary>
        /// Represent unique indentifier for the tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represent name of tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represent fee of entry to the tournament.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// Represent list of teams that take part in tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// Represents list of prize models.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// Represent list of list of matchups.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        public void CompleteTournament()
        {
            OnTournamentComplete?.Invoke(this, DateTime.Now);                               // ? means - to that if its available (if we have subscriber, fire the event )
                                                                                            // generally we just get whatever that just fit here, because
                                                                                            // we can't use the tournament model
        }

    }
}
