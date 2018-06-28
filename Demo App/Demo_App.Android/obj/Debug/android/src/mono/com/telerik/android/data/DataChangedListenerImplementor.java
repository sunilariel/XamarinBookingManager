package mono.com.telerik.android.data;


public class DataChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.data.DataChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dataChanged:(Lcom/telerik/android/data/DataChangeInfo;)V:GetDataChanged_Lcom_telerik_android_data_DataChangeInfo_Handler:Com.Telerik.Android.Data.IDataChangedListenerInvoker, Telerik.Xamarin.Android.Data\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Data.IDataChangedListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", DataChangedListenerImplementor.class, __md_methods);
	}


	public DataChangedListenerImplementor ()
	{
		super ();
		if (getClass () == DataChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Data.IDataChangedListenerImplementor, Telerik.Xamarin.Android.Data, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void dataChanged (com.telerik.android.data.DataChangeInfo p0)
	{
		n_dataChanged (p0);
	}

	private native void n_dataChanged (com.telerik.android.data.DataChangeInfo p0);

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
