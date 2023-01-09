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

    }
}
