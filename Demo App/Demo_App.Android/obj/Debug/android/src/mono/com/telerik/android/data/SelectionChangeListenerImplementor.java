package mono.com.telerik.android.data;


public class SelectionChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.data.SelectionChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_selectionChanged:(Lcom/telerik/android/data/SelectionChangeInfo;)V:GetSelectionChanged_Lcom_telerik_android_data_SelectionChangeInfo_Handler:Com.Telerik.Android.Data.ISelectionChangeListenerInvoker, Telerik.Xamarin.Android.Data\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Data.ISelectionChangeListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", SelectionChangeListenerImplementor.class, __md_methods);
	}


	public SelectionChangeListenerImplementor ()
	{
		super ();
		if (getClass () == SelectionChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Data.ISelectionChangeListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void selectionChanged (com.telerik.android.data.SelectionChangeInfo p0)
	{
		n_selectionChanged (p0);
	}

	private native void n_selectionChanged (com.telerik.android.data.SelectionChangeInfo p0);

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
