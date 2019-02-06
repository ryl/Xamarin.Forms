using System;
using System.Collections.Generic;
using System.Reflection;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Xaml
{
	// IMPORTANT!!
	//  READ BEFORE MODIFYING!!
	// This class represents the API that the Forms previewer will use
	//  via reflection. Adding members or changing implementations is fine, but
	//  DO NOT break this API unless you know what you're doing and/or are
	//  coordinating with the designer team.
	// Thanks!
	static class DesignerInterface
	{
		static Func<ResourceLoader.ResourceLoadingQuery, ResourceLoader.ResourceLoadingResponse> ResourceProvider2 {
			get => ResourceLoader.ResourceProvider2;
			set => ResourceLoader.ResourceProvider2 = value;
		}

		static Action<Exception> ExceptionHandler {
			get => ResourceLoader.ExceptionHandler;
			set => ResourceLoader.ExceptionHandler = value;
		}

		static Func<IList<XamlLoader.FallbackTypeInfo>, Type, Type> FallbackTypeResolver {
			get => XamlLoader.FallbackTypeResolver;
			set => XamlLoader.FallbackTypeResolver = value;
		}

		static Action<XamlLoader.CallbackTypeInfo, object> ValueCreatedCallback  {
			get => XamlLoader.ValueCreatedCallback;
			set => XamlLoader.ValueCreatedCallback = value;
		}

		static void SetIdiom(TargetIdiom idiom) => Device.SetIdiom(idiom);

		static TXaml LoadFromXaml<TXaml>(TXaml view, Type callingType)
			=> Extensions.LoadFromXaml(view, callingType);

		static object CreateFromXaml(string xaml, bool doNotThrow, bool useDesignProperties)
			=> XamlLoader.Create(xaml, doNotThrow, useDesignProperties);

		static string GetPathForType(Type type)
			=> XamlResourceIdAttribute.GetPathForType(type);

		static void RegisterAll(Type[] attrTypes)
			=> Xamarin.Forms.Internals.Registrar.RegisterAll (attrTypes);

		// Additional types/members:
		//  Note that visibility is irrelevant to our concerns; they may be made non-public
		//  without breaking this contract.

		// Types:
		//  Xamarin.Forms.Rectangle
		//  Xamarin.Forms.ExportRendererAttribute
		//  Xamarin.Forms.ExportCellAttribute
		//  Xamarin.Forms.ExportImageSourceHandlerAttribute

		// Platform members:

		// All platforms:
		//  static void SetFlags(string[] flags)
		//  static IVisualElementRenderer Platform.CreateRenderer(VisualElement element)
		//  static void Platform.SetRenderer(VisualElement bindable, IVisualElementRenderer value)

		// iOS:
		//  static void Forms.Init()

		// Android:
		//  static bool Forms.IsInitialized { get; }
		//  static bool FileImageSourceHandler.DecodeSynchronously { set; }
		//  static void Platform.SetPageContext(BindableObject bindable, Context context)
		//  void IVisualElementRenderer.UpdateLayout()
		//  ViewGroup IVisualElementRenderer.ViewGroup { get; }*
		//  static void Forms.Init(Context activity, Bundle bundle)*
	}
}
