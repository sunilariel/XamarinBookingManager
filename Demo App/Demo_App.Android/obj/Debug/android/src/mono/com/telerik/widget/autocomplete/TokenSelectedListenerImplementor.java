package mono.com.telerik.widget.autocomplete;


public class TokenSelectedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.autocomplete.TokenSelectedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenSelected:(Lcom/telerik/widget/autocomplete/RadAutoCompleteTextView;Lcom/telerik/widget/autocomplete/TokenModel;)V:GetOnTokenSelected_Lcom_telerik_widget_autocomplete_RadAutoCompleteTextView_Lcom_telerik_widget_autocomplete_TokenModel_Handler:Com.Telerik.Widget.Autocomplete.ITokenSelectedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Autocomplete.ITokenSelectedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", TokenSelectedListenerImplementor.class, __md_methods);
	}


	public TokenSelectedListenerImplementor ()
	{
		super ();
		if (getClass () == TokenSelectedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Autocomplete.ITokenSelectedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenSelected (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, com.telerik.widget.autocomplete.TokenModel p1)
	{
		n_onTokenSelected (p0, p1);
	}

	private native void n_onTokenSelected (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, com.telerik.widget.autocomplete.TokenModel p1);

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
