using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocketIOapp
{
	public partial class MainPage : ContentPage
	{
        public Socket SocketIOvar { get; set; }
        //pri pouziti openode hostingu tuto adresu zjistite kliknutim na nazev vaseho hostingu
        public static string ServerURL = "http://socketioexamplexamarinapp.fr.openode.io/";
        List<string> ConnectedUsers = new List<string>();
        public MainPage()
		{
			InitializeComponent();

        }

        private void ConnectionToTheServer_Clicked(object sender, EventArgs e)
        {
            connectionFormSL.IsVisible = false;
            SocketIOvar = IO.Socket(ServerURL);
            SocketIOvar.On(Socket.EVENT_CONNECT, () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    afterConnectionToServerSL.IsVisible = true;
                    yourNicknameLBL.Text = "tvůj nickname je: " + yourNicknameEntry.Text;
                });

                SocketIOvar.Emit("connectToServer", yourNicknameEntry.Text);
                
                SocketIOvar.On("viewNewlyConnected", (newlyConnectedUser) =>
                {
                    string newUser = newlyConnectedUser.ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        connectedUsersLV.ItemsSource = null;
                        ConnectedUsers.Add(newUser);
                        connectedUsersLV.ItemsSource = ConnectedUsers;
                    });
                });
            });
        }
    }
}
