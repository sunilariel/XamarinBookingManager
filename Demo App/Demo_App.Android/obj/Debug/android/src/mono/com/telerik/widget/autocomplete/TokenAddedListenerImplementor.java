package mono.com.telerik.widget.autocomplete;


public class TokenAddedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.autocomplete.TokenAddedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenAdded:(Lcom/telerik/widget/autocomplete/RadAutoCompleteTextView;Lcom/telerik/widget/autocomplete/TokenModel;)V:GetOnTokenAdded_Lcom_telerik_widget_autocomplete_RadAutoCompleteTextView_Lcom_telerik_widget_autocomplete_TokenModel_Handler:Com.Telerik.Widget.Autocomplete.ITokenAddedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Autocomplete.ITokenAddedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", TokenAddedListenerImplementor.class, __md_methods);
	}


	public TokenAddedListenerImplementor ()
	{
		super ();
		if (getClass () == TokenAddedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Autocomplete.ITokenAddedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenAdded (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, com.telerik.widget.autocomplete.TokenModel p1)
	{
		n_onTokenAdded (p0, p1);
	}

	private native void n_onTokenAdded (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, com.telerik.widget.autocomplete.TokenModel p1);

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
