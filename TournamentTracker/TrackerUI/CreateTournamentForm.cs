using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;

            selectTeamDropDown.DataSource = availableTeams;                                     //DataSource takes the data from the our database or text file
            selectTeamDropDown.DisplayMember = "TeamName";                                      //and DisplayMember shows the value we want in that ListBox

            tournamentTeamsListBox.DataSource = null;

            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;

            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the CreatePrizeForm
            CreatePrizeForm frm = new CreatePrizeForm(this);                                      //this keyword represents this specific instance
            frm.Show();                                                                           //beeing CreateTournamentForm class but more importantly
                                                                                                  //for CreatePrizeForm, but also IPrizeRequester "contract" fullfiller.

        }

        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            } 
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);

                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            
            // Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable || fee < 0)
            {
                MessageBox.Show("You need to enter a valid Entry fee.",
                    "Invalid Fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Create our tournament model
            TournamentModel tm = new TournamentModel();

            if (tournamentNameValue.Text.Length > 0)
            {
                tm.TournamentName = tournamentNameValue.Text;

            }
            else
            {
                MessageBox.Show("Please enter a valid Tournament Name.", "Invalid Tournament Name");
                return;
            }

            tm.EntryFee = fee;
            tm.Prizes = selectedPrizes;

            if (selectedTeams.Count < 2)
            {
                MessageBox.Show("Please register at least two teams in the tournament", "Invalid Team Count");
                return;
            }
            else
            {
                tm.EnteredTeams = selectedTeams;
            }

            // Wire up our matchups 
            TournamentLogic.CreateRounds(tm);

            // Create Tournament entry
            // Create all of the prizes entries
            // Create all of team entries
            GlobalConfig.Connection.CreateTournament(tm);

            // Emailing feature currently disabled.
            //tm.AlertUsersToNewRound();

            //TournamentLogic.UpdateTournamentResults(tm);

            TournamentViewerForm frm = new TournamentViewerForm(tm);
            frm.Show();
            this.Close();


        }
    }
}
