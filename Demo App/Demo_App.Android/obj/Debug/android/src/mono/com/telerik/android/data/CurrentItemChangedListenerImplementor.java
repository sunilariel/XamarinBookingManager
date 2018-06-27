package mono.com.telerik.android.data;


public class CurrentItemChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.data.CurrentItemChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_currentItemChanged:(Lcom/telerik/android/data/CurrentItemChangedInfo;)V:GetCurrentItemChanged_Lcom_telerik_android_data_CurrentItemChangedInfo_Handler:Com.Telerik.Android.Data.ICurrentItemChangedListenerInvoker, Telerik.Xamarin.Android.Data\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Data.ICurrentItemChangedListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", CurrentItemChangedListenerImplementor.class, __md_methods);
	}


	public CurrentItemChangedListenerImplementor ()
	{
		super ();
		if (getClass () == CurrentItemChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Data.ICurrentItemChangedListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void currentItemChanged (com.telerik.android.data.CurrentItemChangedInfo p0)
	{
		n_currentItemChanged (p0);
	}

	private native void n_currentItemChanged (com.telerik.android.data.CurrentItemChangedInfo p0);

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
