using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAcces.TextHelpers;

namespace TrackerLibrary.DataAcces
{
    public class TextConnector : IDataConnection
    {
        // TODO - Wire up the CreatePrize for text files.
        /// <summary>
        /// Saves a new prize to the text file.
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique indentifier.</returns>

        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and Convert the text to  List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the ID
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            currentId += 1;

            // Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // Convert the prizes to list<string>
            //Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);

            return model;

        }
        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

        }

        public void CreateTournament(TournamentModel model)
        {
            
            List<TournamentModel> tournaments = TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);

            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            model.SaveRoundsToFile(MatchupFile, MatchupEntryFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);
        }

        public List<TournamentModel> GetTournament_All()
        {
            return TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);
        }
    }
}
