#if __ANDROID_28__
using System;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
namespace Xamarin.Forms.Platform.Android.Material
{
	public class MaterialPickerEditText : TextInputEditText, IPopupTrigger
	{
		public bool ShowPopupOnFocus { get; set; }

		public MaterialPickerEditText(Context context) : base(context)
		{
			PickerEditTextManager.Init(Context, this);
			MaterialFormsEditTextManager.Init(this);
		}

		protected MaterialPickerEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			PickerEditTextManager.Init(Context, this);
			MaterialFormsEditTextManager.Init(this);
		}

		public MaterialPickerEditText(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			PickerEditTextManager.Init(Context, this);
			MaterialFormsEditTextManager.Init(this);
		}

		public MaterialPickerEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
			PickerEditTextManager.Init(Context, this);
			MaterialFormsEditTextManager.Init(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				PickerEditTextManager.Dispose(this);
				MaterialFormsEditTextManager.Dispose(this);
			}

			base.Dispose(disposing);
		}
	}
}
#endif