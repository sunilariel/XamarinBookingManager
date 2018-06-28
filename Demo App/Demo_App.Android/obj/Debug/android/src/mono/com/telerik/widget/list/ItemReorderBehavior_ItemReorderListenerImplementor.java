package mono.com.telerik.widget.list;


public class ItemReorderBehavior_ItemReorderListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.ItemReorderBehavior.ItemReorderListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReorderFinished:()V:GetOnReorderFinishedHandler:Com.Telerik.Widget.List.ItemReorderBehavior/IItemReorderListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onReorderItem:(II)V:GetOnReorderItem_IIHandler:Com.Telerik.Widget.List.ItemReorderBehavior/IItemReorderListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onReorderStarted:(I)V:GetOnReorderStarted_IHandler:Com.Telerik.Widget.List.ItemReorderBehavior/IItemReorderListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.ItemReorderBehavior+IItemReorderListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", ItemReorderBehavior_ItemReorderListenerImplementor.class, __md_methods);
	}


	public ItemReorderBehavior_ItemReorderListenerImplementor ()
	{
		super ();
		if (getClass () == ItemReorderBehavior_ItemReorderListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.ItemReorderBehavior+IItemReorderListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReorderFinished ()
	{
		n_onReorderFinished ();
	}

	private native void n_onReorderFinished ();


	public void onReorderItem (int p0, int p1)
	{
		n_onReorderItem (p0, p1);
	}

	private native void n_onReorderItem (int p0, int p1);


	public void onReorderStarted (int p0)
	{
		n_onReorderStarted (p0);
	}

	private native void n_onReorderStarted (int p0);

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
