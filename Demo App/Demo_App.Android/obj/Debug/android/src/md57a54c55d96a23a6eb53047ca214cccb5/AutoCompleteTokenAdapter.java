package md57a54c55d96a23a6eb53047ca214cccb5;


public class AutoCompleteTokenAdapter
	extends com.telerik.widget.autocomplete.TokenAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getViewForTokenObject:(Lcom/telerik/widget/autocomplete/TokenModel;)Lcom/telerik/widget/autocomplete/TokenView;:GetGetViewForTokenObject_Lcom_telerik_widget_autocomplete_TokenModel_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTokenAdapter, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AutoCompleteTokenAdapter.class, __md_methods);
	}


	public AutoCompleteTokenAdapter (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AutoCompleteTokenAdapter.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AutoComplete.AutoCompleteTokenAdapter, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public com.telerik.widget.autocomplete.TokenView getViewForTokenObject (com.telerik.widget.autocomplete.TokenModel p0)
	{
		return n_getViewForTokenObject (p0);
	}

	private native com.telerik.widget.autocomplete.TokenView n_getViewForTokenObject (com.telerik.widget.autocomplete.TokenModel p0);

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
