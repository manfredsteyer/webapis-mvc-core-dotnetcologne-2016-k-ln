using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thinktecture.IdentityModel.Client;

namespace TokenClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task<string> ReceiveToken()
        {
            var idsUrl = "https://steyer-identity-server.azurewebsites.net/identity/connect/token";
            var oauthClient = new OAuth2Client(
                                    address: new Uri(idsUrl),
                                    clientId: "demo-resource-owner",
                                    clientSecret: "geheim");

            var tokenResponse = await oauthClient.RequestResourceOwnerPasswordAsync(
                                    userName: this.txtUserName.Text,
                                    password: this.txtPassword.Text,
                                    scope: "openid profile email booking");

            Debug.WriteLine("Access Token: " + tokenResponse.AccessToken);
            var accessToken = tokenResponse.AccessToken;


            return accessToken;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var token = await ReceiveToken();
            this.txtToken.Text = token;
        }
    }
}
