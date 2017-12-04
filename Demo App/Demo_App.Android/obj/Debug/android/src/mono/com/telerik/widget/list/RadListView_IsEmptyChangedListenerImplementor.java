package mono.com.telerik.widget.list;


public class RadListView_IsEmptyChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.RadListView.IsEmptyChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:(Z)V:GetOnChanged_ZHandler:Com.Telerik.Widget.List.RadListView/IIsEmptyChangedListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.RadListView+IIsEmptyChangedListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", RadListView_IsEmptyChangedListenerImplementor.class, __md_methods);
	}


	public RadListView_IsEmptyChangedListenerImplementor ()
	{
		super ();
		if (getClass () == RadListView_IsEmptyChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.RadListView+IIsEmptyChangedListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onChanged (boolean p0)
	{
		n_onChanged (p0);
	}

	private native void n_onChanged (boolean p0);

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
