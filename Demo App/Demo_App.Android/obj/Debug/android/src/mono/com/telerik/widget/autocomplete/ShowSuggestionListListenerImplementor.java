package mono.com.telerik.widget.autocomplete;


public class ShowSuggestionListListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.autocomplete.ShowSuggestionListListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onShowSuggestionList:(Lcom/telerik/widget/autocomplete/RadAutoCompleteTextView;Ljava/util/List;)V:GetOnShowSuggestionList_Lcom_telerik_widget_autocomplete_RadAutoCompleteTextView_Ljava_util_List_Handler:Com.Telerik.Widget.Autocomplete.IShowSuggestionListListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Autocomplete.IShowSuggestionListListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", ShowSuggestionListListenerImplementor.class, __md_methods);
	}


	public ShowSuggestionListListenerImplementor ()
	{
		super ();
		if (getClass () == ShowSuggestionListListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Autocomplete.IShowSuggestionListListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onShowSuggestionList (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, java.util.List p1)
	{
		n_onShowSuggestionList (p0, p1);
	}

	private native void n_onShowSuggestionList (com.telerik.widget.autocomplete.RadAutoCompleteTextView p0, java.util.List p1);

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
