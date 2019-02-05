#if __ANDROID_28__
using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.Design.Widget;
using Android.Runtime;
using Android.Util;


namespace Xamarin.Forms.Platform.Android.Material
{

	public class MaterialFormsTextInputLayout : TextInputLayout
	{
		public MaterialFormsTextInputLayout(Context context) : base(context)
		{
			Init();
		}

		public MaterialFormsTextInputLayout(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Init();
		}

		public MaterialFormsTextInputLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
			Init();
		}

		protected MaterialFormsTextInputLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			Init();
		}

		void Init()
		{
			VisualElement.VerifyVisualFlagEnabled();
		}

	}

	internal static class MaterialFormsEditTextManager
	{

		// These paddings are a hack to center the hint
		// once this issue is resolved we can get rid of these paddings
		// https://github.com/material-components/material-components-android/issues/120
		// https://stackoverflow.com/questions/50487871/how-to-make-the-hint-text-of-textinputlayout-vertically-center

		static Thickness _centeredText = new Thickness(16, 8, 12, 27);
		static Thickness _alignedWithUnderlineText = new Thickness(16, 20, 12, 16);

		public static void Init(TextInputEditText textInputEditText)
		{
			VisualElement.VerifyVisualFlagEnabled();

			textInputEditText.TextChanged += OnTextChanged;
			textInputEditText.FocusChange += OnFocusChanged;
		}

		public static void Dispose(TextInputEditText textInputEditText)
		{
			textInputEditText.TextChanged -= OnTextChanged;
			textInputEditText.FocusChange -= OnFocusChanged;
		}

		private static void OnFocusChanged(object sender, global::Android.Views.View.FocusChangeEventArgs e)
		{
			if (sender is TextInputEditText textInputEditText)
			{
				// Delay padding update until after the keyboard has showed up otherwise updating the padding
				// stops the keyboard from showing up
				// TODO closure
				if (e.HasFocus)
					Device.BeginInvokeOnMainThread(() => UpdatePadding(textInputEditText));
				else
					UpdatePadding(textInputEditText);
			}
		}

		private static void OnTextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
		{
			if (e.BeforeCount == 0 || e.AfterCount == 0)
				UpdatePadding(sender as TextInputEditText);
		}

		static void UpdatePadding(TextInputEditText textInputEditText)
		{
			Thickness rect = _centeredText;

			if (!String.IsNullOrWhiteSpace(textInputEditText.Text) || textInputEditText.HasFocus)
			{
				rect = _alignedWithUnderlineText;
			}

			Context Context = textInputEditText.Context;
			textInputEditText.SetPadding((int)Context.ToPixels(rect.Left), (int)Context.ToPixels(rect.Top), (int)Context.ToPixels(rect.Right), (int)Context.ToPixels(rect.Bottom));
		}
	}

	public class MaterialFormsEditText : TextInputEditText, IDescendantFocusToggler, IFormsEditText
	{
		DescendantFocusToggler _descendantFocusToggler;

		public MaterialFormsEditText(Context context) : base(context)
		{
			MaterialFormsEditTextManager.Init(this);
		}

		protected MaterialFormsEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			MaterialFormsEditTextManager.Init(this);
		}

		public MaterialFormsEditText(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			MaterialFormsEditTextManager.Init(this);
		}

		public MaterialFormsEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
			MaterialFormsEditTextManager.Init(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				MaterialFormsEditTextManager.Dispose(this);

			base.Dispose(disposing);
		}

		bool IDescendantFocusToggler.RequestFocus(global::Android.Views.View control, Func<bool> baseRequestFocus)
		{
			_descendantFocusToggler = _descendantFocusToggler ?? new DescendantFocusToggler();

			return _descendantFocusToggler.RequestFocus(control, baseRequestFocus);
		}

		public override bool OnKeyPreIme(Keycode keyCode, KeyEvent e)
		{
			if (keyCode != Keycode.Back || e.Action != KeyEventActions.Down)
			{
				return base.OnKeyPreIme(keyCode, e);
			}

			this.HideKeyboard();

			_onKeyboardBackPressed?.Invoke(this, EventArgs.Empty);
			return true;
		}

		public override bool RequestFocus(FocusSearchDirection direction, Rect previouslyFocusedRect)
		{
			return (this as IDescendantFocusToggler).RequestFocus(this, () => base.RequestFocus(direction, previouslyFocusedRect));
		}

		protected override void OnSelectionChanged(int selStart, int selEnd)
		{
			base.OnSelectionChanged(selStart, selEnd);
			_selectionChanged?.Invoke(this, new SelectionChangedEventArgs(selStart, selEnd));
		}

		event EventHandler _onKeyboardBackPressed;
		event EventHandler IFormsEditText.OnKeyboardBackPressed
		{
			add => _onKeyboardBackPressed += value;
			remove => _onKeyboardBackPressed -= value;
		}

		event EventHandler<SelectionChangedEventArgs> _selectionChanged;
		event EventHandler<SelectionChangedEventArgs> IFormsEditText.SelectionChanged
		{
			add => _selectionChanged += value;
			remove => _selectionChanged -= value;
		}
	}
}
#endif