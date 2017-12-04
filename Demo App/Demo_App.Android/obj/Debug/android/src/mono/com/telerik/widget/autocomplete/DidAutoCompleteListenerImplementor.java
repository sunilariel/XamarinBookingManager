package mono.com.telerik.widget.autocomplete;


public class DidAutoCompleteListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.autocomplete.DidAutoCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDidAutoComplete:(Lcom/telerik/widget/autocomplete/RadAutoCompleteTextView;Ljava/lang/String;)V:GetOnDidAutoComplete_Lcom_telerik_widget_autocomplete_RadAutoCompleteTextView_Ljava_lang_String_Handler:Com.Telerik.Widget.Autocomplete.IDidAutoCompleteListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Autocomplete.IDidAutoCompleteListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", DidAutoCompleteListenerImplementor.class, __md_methods);
	}


	public DidAutoCompleteListenerImplementor ()
	{
		super ();
		if (getClass () == DidAutoCompleteListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Autocomplete.IDidAutoCompleteListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDidAutoComplete (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, java.lang.String p1)
	{
		n_onDidAutoComplete (p0, p1);
	}

	private native void n_onDidAutoComplete (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, java.lang.String p1);

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
