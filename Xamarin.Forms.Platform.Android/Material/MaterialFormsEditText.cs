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