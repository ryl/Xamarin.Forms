#if __ANDROID_28__
using System;
using Android.Content;
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
}
#endif