using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Text;
using Java.Lang;
using System.Collections.Generic;
using Android.Graphics;
using Android.Runtime;
using AView = global::Android.Views.View;
using System;

namespace Xamarin.Forms.Platform.Android
{
	public static class PickerEditTextManager
	{
		readonly static HashSet<Keycode> availableKeys = new HashSet<Keycode>(new[] {
			Keycode.Tab, Keycode.Forward, Keycode.Back, Keycode.DpadDown, Keycode.DpadLeft, Keycode.DpadRight, Keycode.DpadUp
		});

		public static void Setup(Context context, EditText editText)
		{
			editText.Focusable = true;
			editText.Clickable = true;
			editText.InputType = InputTypes.Null;
			editText.KeyPress += OnKeyPress;
			editText.FocusChange += OnFocusChange;
			editText.Touch += OnTouchEvent;
			editText.SetOnClickListener(PickerListener.Instance);
		}

		static void OnTouchEvent(object sender, AView.TouchEventArgs e)
		{
			if (sender is PickerEditText view)
			{
				if (e.Event.Action == MotionEventActions.Up && !view.IsFocused)
				{
					view.RequestFocus();
				}

				e.Handled = false;
			}
		}

		static void OnFocusChange(object sender, AView.FocusChangeEventArgs e)
		{
			if (sender is AView view && sender is IPopupTrigger popupTrigger)
			{
				// Todo check if gain focus is different
				if (e.HasFocus && popupTrigger.ShowPopupOnFocus)
					view.CallOnClick();

				popupTrigger.ShowPopupOnFocus = false;
			}
		}

		static void OnKeyPress(object sender, AView.KeyEventArgs e)
		{
			if (availableKeys.Contains(e.KeyCode))
			{
				e.Handled = false;
				return;
			}
			e.Handled = true;
			(sender as AView)?.CallOnClick();
		}

		public static void Dispose(EditText editText)
		{
			editText.KeyPress -= OnKeyPress;
			editText.FocusChange -= OnFocusChange;
			editText.Touch -= OnTouchEvent;
			editText.SetOnClickListener(null);
		}

		class PickerListener : global::Java.Lang.Object, AView.IOnClickListener
		{
			public static readonly PickerListener Instance = new PickerListener();

			public void OnClick(global::Android.Views.View v)
			{
				if (v is PickerEditText picker)
				{
					if (picker?.Parent is IPickerRenderer renderer1)
						renderer1.OnClick();
					else if (picker?.Parent?.Parent is IPickerRenderer renderer2)
						renderer2.OnClick();
					else
						throw new System.Exception("Renderer not found temp check for Shane things");
				}
			}
		}
	}


	public class PickerEditText : EditText, IPopupTrigger
	{
		public bool ShowPopupOnFocus { get; set; }

		public PickerEditText(Context context, IPickerRenderer pickerRenderer) : base(context)
		{
			PickerEditTextManager.Setup(Context, this);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				PickerEditTextManager.Dispose(this);

			base.Dispose(disposing);
		}
	}
}