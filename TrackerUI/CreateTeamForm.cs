using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequester callingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            // CreateSampleData();

            WireUpLists();
        }



        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Yishay", Lastname = "Garame" });
            availableTeamMembers.Add(new PersonModel { FirstName = "David", Lastname = "King" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Haim", Lastname = "Moshe" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Zohar", Lastname = "Argov" });
        }

        private void WireUpLists()
        {

            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PersonModel person = new PersonModel();
                person.FirstName = firstNameValue.Text;
                person.Lastname = lastNameValue.Text;
                person.EmailAddress = emailValue.Text;
                person.CellPhoneNumber = cellphoneValue.Text;

                person = GlobalConfig.Connection.CreatePerson(person);

                selectedTeamMembers.Add(person);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all the fields.");
            }
        }

        private bool ValidateForm()
        {
            if(firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;
            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }

        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;
            if(p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();

            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel teamModel = new TeamModel();
            teamModel.TeamName = teamNameValue.Text;
            teamModel.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(teamModel);

            callingForm.TeamComplete(teamModel);
            this.Close();
            // TODO - check for name team that is not empty in both sql and text
        }
    }
}
