package mono.com.telerik.widget.dataform.engine;


public class PropertyChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.PropertyChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPropertyChanged:(Ljava/lang/String;Ljava/lang/Object;)V:GetOnPropertyChanged_Ljava_lang_String_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IPropertyChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", PropertyChangedListenerImplementor.class, __md_methods);
	}


	public PropertyChangedListenerImplementor ()
	{
		super ();
		if (getClass () == PropertyChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPropertyChanged (java.lang.String p0, java.lang.Object p1)
	{
		n_onPropertyChanged (p0, p1);
	}

	private native void n_onPropertyChanged (java.lang.String p0, java.lang.Object p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
