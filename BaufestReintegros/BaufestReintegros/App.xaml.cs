using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BaufestReintegros
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            NavigationPage nav = new NavigationPage(new View.Login());
            MainPage = nav;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
