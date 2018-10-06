using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;

namespace FB_Full_Access
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string accessToken;

        private async void bFBLogin_Click(object sender, EventArgs e)
        {
            FBDialog fbd = new FBDialog(tbAppID.Text, tbScopes.Text);

            switch (fbd.ShowDialog(this))
            {
                case DialogResult.Abort:    // There was an error
                    // Get the error information
                    lbResult.Items.Add(string.Format("Error: {0}", fbd.error));
                    lbResult.Items.Add(string.Format("Error reason: {0}", fbd.error_reason));
                    lbResult.Items.Add(string.Format("Error description: {0}", fbd.error_description));
                    MessageBox.Show("There was an error or the user denied access! See log window for more information!", "Error: An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case DialogResult.Cancel:   // User clicked cancel or closed the dialog
                    MessageBox.Show("The user clicked cancel or closed the dialog!", "Error: Interupted by user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case DialogResult.OK:   // Logon successfull
                    lbResult.Items.Add(string.Format("Access token: {0}", fbd.access_token));
                    lbResult.Items.Add(string.Format("Token expires: {0}", fbd.token_expires));
                    lbResult.Items.Add(string.Format("Granted scopes: {0}", fbd.granted_scopes));
                    lbResult.Items.Add(string.Format("Denied scopes: {0}", fbd.denied_scopes));
                    //MessageBox.Show("User login was successfull! See log window for more information!", "Successfull!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    accessToken = fbd.access_token;

                    FacebookClient fbCLient = new FacebookClient();
                    var facebookService = new FacebookService(fbCLient);
                    var getAccountTask = facebookService.GetAccountAsync(accessToken);
                    //var account = getAccountTask.Result;
                    //Task.WaitAll(getAccountTask);
                    var account = await facebookService.GetAccountAsync(accessToken);
                    label6.Text = account.Id;
                    label7.Text = account.Name;
                    label8.Text = account.Email;

                    break;
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
