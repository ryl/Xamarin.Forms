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
			MainPage = CreateStackLayoutPage(new View[] { new DatePicker(), new TimePicker(), new Picker() { Items = { "1", "2", "3" } } });
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
