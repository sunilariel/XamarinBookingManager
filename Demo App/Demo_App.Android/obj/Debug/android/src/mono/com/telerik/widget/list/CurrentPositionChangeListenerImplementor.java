package mono.com.telerik.widget.list;


public class CurrentPositionChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.CurrentPositionChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCurrentPositionChanged:(II)V:GetOnCurrentPositionChanged_IIHandler:Com.Telerik.Widget.List.ICurrentPositionChangeListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.ICurrentPositionChangeListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", CurrentPositionChangeListenerImplementor.class, __md_methods);
	}


	public CurrentPositionChangeListenerImplementor ()
	{
		super ();
		if (getClass () == CurrentPositionChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.ICurrentPositionChangeListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCurrentPositionChanged (int p0, int p1)
	{
		n_onCurrentPositionChanged (p0, p1);
	}

	private native void n_onCurrentPositionChanged (int p0, int p1);

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
