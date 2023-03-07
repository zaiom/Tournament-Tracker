using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one team in the matchup.
    /// </summary>
    public class MatchupEntryModel
    {
        /// <summary>
        /// The unique identifier of the matchup entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique indentifier for the team.
        /// </summary>
        public int TeamCompetingId { get; set; }

        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The unique identifier for the parent matchup (team).
        /// </summary>
        public int ParentMatchupId { get; set; }

        /// <summary>
        /// Represents the matchup that this team came 
        /// from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
