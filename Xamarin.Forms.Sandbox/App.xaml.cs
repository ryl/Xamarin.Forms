using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.Forms.Sandbox
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Device.SetFlags(new[] { "Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental" });

			var picker = new Picker() { TitleColor = Color.Green, Title = "I am a title", Items = { "1", "2", "3" } };
			var button = new Button()
			{
				Text = "Clear",
				Command = new Command(() =>
				{
					picker.SelectedItem = null;
				})
			};

			MainPage = CreateStackLayoutPage(new View[] { new DatePicker(), new TimePicker(), picker, button, new Entry(), new Entry() { Placeholder = "I am a title" }, new Entry(), new Entry() });
		}

		ContentPage CreateStackLayoutPage(IEnumerable<View> children)
		{
			var sl = new StackLayout();
			foreach (var child in children)
				sl.Children.Add(child);

			return new ContentPage()
			{
				Content = sl,
				Visual = VisualMarker.Material
			};
		}
	}
}
