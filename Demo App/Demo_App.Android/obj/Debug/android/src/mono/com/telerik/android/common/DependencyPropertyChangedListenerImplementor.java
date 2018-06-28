package mono.com.telerik.android.common;


public class DependencyPropertyChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.common.DependencyPropertyChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPropertyChanged:(Ljava/lang/Object;ILjava/lang/Object;)V:GetOnPropertyChanged_Ljava_lang_Object_ILjava_lang_Object_Handler:Com.Telerik.Android.Common.IDependencyPropertyChangedListenerInvoker, Telerik.Xamarin.Android.Common\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Common.IDependencyPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Common, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", DependencyPropertyChangedListenerImplementor.class, __md_methods);
	}


	public DependencyPropertyChangedListenerImplementor ()
	{
		super ();
		if (getClass () == DependencyPropertyChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Common.IDependencyPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Common, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPropertyChanged (java.lang.Object p0, int p1, java.lang.Object p2)
	{
		n_onPropertyChanged (p0, p1, p2);
	}

	private native void n_onPropertyChanged (java.lang.Object p0, int p1, java.lang.Object p2);

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
