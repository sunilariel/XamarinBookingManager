package mono.com.telerik.widget.list;


public class SwipeRefreshBehavior_SwipeRefreshListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.SwipeRefreshBehavior.SwipeRefreshListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRefreshRequested:()V:GetOnRefreshRequestedHandler:Com.Telerik.Widget.List.SwipeRefreshBehavior/ISwipeRefreshListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.SwipeRefreshBehavior+ISwipeRefreshListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", SwipeRefreshBehavior_SwipeRefreshListenerImplementor.class, __md_methods);
	}


	public SwipeRefreshBehavior_SwipeRefreshListenerImplementor ()
	{
		super ();
		if (getClass () == SwipeRefreshBehavior_SwipeRefreshListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.SwipeRefreshBehavior+ISwipeRefreshListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onRefreshRequested ()
	{
		n_onRefreshRequested ();
	}

	private native void n_onRefreshRequested ();

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
