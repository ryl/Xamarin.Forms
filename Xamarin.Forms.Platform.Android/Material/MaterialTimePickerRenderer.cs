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

		public MaterialTimePickerRenderer(Context context) : base(context)
		{
		}

		protected override EditText EditText => _textInputEditText;

		protected override MaterialFormsTextInputLayout CreateNativeControl()
		{
			LayoutInflater inflater = LayoutInflater.FromContext(Context);
			var view = inflater.Inflate(Resource.Layout.MaterialPickerTextInput, null);
			_textInputLayout = (MaterialFormsTextInputLayout)view;
			_textInputEditText = _textInputLayout.FindViewById<MaterialPickerEditText>(Resource.Id.materialformsedittext);

			//_textInputEditText.FocusChange += TextInputEditTextFocusChange;
			//_textInputLayout.Hint = Element.Placeholder;

			//return _textInputLayout;
			//return new PickerEditText(Context, this);

			return _textInputLayout;
		}
	}

	//public class MaterialTimePickerRenderer : TextInputLayout,
	//	IVisualElementRenderer, IViewRenderer, ITabStop
	//{

	//	int? _defaultLabelFor;
	//	bool _disposed;
	//	MaterialFormsEditText _textInputEditText;

	//	TimePicker _element;
	//	VisualElementTracker _visualElementTracker;
	//	VisualElementRenderer _visualElementRenderer;
	//	MotionEventHelper _motionEventHelper;

	//	public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
	//	public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

	//	public MaterialTimePickerRenderer(Context context)
	//		: base(new ContextThemeWrapper(context, Resource.Style.XamarinFormsMaterialProgressBarHorizontal), null, Resource.Style.XamarinFormsMaterialProgressBarHorizontal)
	//	{
	//		VisualElement.VerifyVisualFlagEnabled();


	//		_visualElementRenderer = new VisualElementRenderer(this);
	//		_motionEventHelper = new MotionEventHelper();
	//	}


	//	protected virtual MaterialFormsEditText CreateNativeControl()
	//	{
	//		//LayoutInflater inflater = LayoutInflater.FromContext(Context);
	//		//var view = inflater.Inflate(Resource.Layout.TextInputLayoutFilledBox, null);
	//		//_textInputLayout = (MaterialFormsTextInputLayout)view;
	//		//_textInputEditText = _textInputLayout.FindViewById<MaterialFormsEditText>(Resource.Id.materialformsedittext);
	//		_textInputEditText = new MaterialFormsEditText(Context);
	//		//_textInputEditText.FocusChange += TextInputEditTextFocusChange;
	//		//_textInputLayout.Hint = Element.Placeholder;

	//		this.AddView(_textInputEditText);
	//		return _textInputEditText;
	//	}

	//	protected TextInputLayout Control => this;

	//	protected TimePicker Element
	//	{
	//		get { return _element; }
	//		set
	//		{
	//			if (_element == value)
	//				return;

	//			var oldElement = _element;
	//			_element = value;

	//			OnElementChanged(new ElementChangedEventArgs<TimePicker>(oldElement, _element));

	//			_element?.SendViewInitialized(this);

	//			_motionEventHelper.UpdateElement(_element);
	//		}
	//	}

	//	protected override void Dispose(bool disposing)
	//	{
	//		if (_disposed)
	//			return;
	//		_disposed = true;

	//		if (disposing)
	//		{
	//			_visualElementTracker?.Dispose();
	//			_visualElementTracker = null;

	//			_visualElementRenderer?.Dispose();
	//			_visualElementRenderer = null;

	//			if (Element != null)
	//			{
	//				Element.PropertyChanged -= OnElementPropertyChanged;

	//				if (Platform.GetRenderer(Element) == this)
	//					Element.ClearValue(Platform.RendererProperty);
	//			}
	//		}

	//		base.Dispose(disposing);
	//	}

	//	protected virtual void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
	//	{
	//		ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(e.OldElement, e.NewElement));

	//		if (e.OldElement != null)
	//		{
	//			e.OldElement.PropertyChanged -= OnElementPropertyChanged;
	//		}

	//		if (e.NewElement != null)
	//		{
	//			this.EnsureId();

	//			if (_visualElementTracker == null)
	//				_visualElementTracker = new VisualElementTracker(this);

	//			e.NewElement.PropertyChanged += OnElementPropertyChanged;


	//			ElevationHelper.SetElevation(this, e.NewElement);
	//		}
	//	}

	//	protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	//	{
	//		ElementPropertyChanged?.Invoke(this, e);
	//	}

	//	public override bool OnTouchEvent(MotionEvent e)
	//	{
	//		if (_visualElementRenderer.OnTouchEvent(e) || base.OnTouchEvent(e))
	//			return true;

	//		return _motionEventHelper.HandleMotionEvent(Parent, e);
	//	}

	//	// IVisualElementRenderer

	//	VisualElement IVisualElementRenderer.Element => Element;

	//	VisualElementTracker IVisualElementRenderer.Tracker => _visualElementTracker;

	//	ViewGroup IVisualElementRenderer.ViewGroup => null;

	//	AView IVisualElementRenderer.View => this;

	//	SizeRequest IVisualElementRenderer.GetDesiredSize(int widthConstraint, int heightConstraint)
	//	{
	//		Measure(widthConstraint, heightConstraint);
	//		return new SizeRequest(new Size(Control.MeasuredWidth, Control.MeasuredHeight), new Size());
	//	}

	//	void IVisualElementRenderer.SetElement(VisualElement element) =>
	//		Element = (element as TimePicker) ?? throw new ArgumentException("Element must be of type TimePicker.");

	//	void IVisualElementRenderer.SetLabelFor(int? id)
	//	{
	//		if (_defaultLabelFor == null)
	//			_defaultLabelFor = ViewCompat.GetLabelFor(this);

	//		ViewCompat.SetLabelFor(this, (int)(id ?? _defaultLabelFor));
	//	}

	//	void IVisualElementRenderer.UpdateLayout() =>
	//		_visualElementTracker?.UpdateLayout();

	//	// IViewRenderer

	//	void IViewRenderer.MeasureExactly() =>
	//		ViewRenderer.MeasureExactly(this, Element, Context);

	//	// ITabStop

	//	AView ITabStop.TabStop => this;
	//}
}
#endif