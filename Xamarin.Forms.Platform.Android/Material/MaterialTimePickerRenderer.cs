#if __ANDROID_28__
using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.FastRenderers;
using Xamarin.Forms.Platform.Android.Material;
using AColor = Android.Graphics.Color;
using AProgressBar = Android.Widget.ProgressBar;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(Xamarin.Forms.TimePicker), typeof(MaterialTimePickerRenderer), new[] { typeof(VisualRendererMarker.Material) })]

namespace Xamarin.Forms.Platform.Android.Material
{
	public class MaterialTimePickerRenderer : TimePickerRendererBase<MaterialFormsTextInputLayout>
	{
		MaterialFormsTextInputLayout _textInputLayout;
		MaterialPickerEditText _textInputEditText;

		public MaterialTimePickerRenderer(Context context) : base(MaterialContextThemeWrapper.Create(context))
		{
		}

		protected override EditText EditText => _textInputEditText;

		protected override MaterialFormsTextInputLayout CreateNativeControl()
		{
			LayoutInflater inflater = LayoutInflater.FromContext(Context);
			var view = inflater.Inflate(Resource.Layout.MaterialPickerTextInput, null);
			_textInputLayout = (MaterialFormsTextInputLayout)view;
			_textInputEditText = _textInputLayout.FindViewById<MaterialPickerEditText>(Resource.Id.materialformsedittext);
			
			return _textInputLayout;
		}


		protected override void UpdateBackgroundColor()
		{
			if (_textInputLayout == null)
				return;

			_textInputLayout.BoxBackgroundColor = MaterialColors.CreateEntryFilledInputBackgroundColor(Element.BackgroundColor, Element.TextColor);
		}

		protected internal override void UpdateTextColor() => ApplyTheme();

		void ApplyTheme()
		{
			if (_textInputLayout == null)
				return;

			// set text color
			var textColor = MaterialColors.GetEntryTextColor(Element.TextColor);
			UpdateTextColor(Color.FromUint((uint)textColor.ToArgb()));

			var underlineColors = MaterialColors.GetUnderlineColor(Element.TextColor);

			var colors = MaterialColors.CreateEntryUnderlineColors(underlineColors.FocusedColor, underlineColors.UnFocusedColor);

			ViewCompat.SetBackgroundTintList(_textInputEditText, colors);
		}

	}
}
#endif