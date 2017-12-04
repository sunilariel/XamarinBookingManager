package mono.com.telerik.widget.list;


public class CollapsibleGroupsBehavior_CollapseGroupListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.CollapsibleGroupsBehavior.CollapseGroupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGroupCollapsed:(Ljava/lang/Object;)V:GetOnGroupCollapsed_Ljava_lang_Object_Handler:Com.Telerik.Widget.List.CollapsibleGroupsBehavior/ICollapseGroupListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onGroupExpanded:(Ljava/lang/Object;)V:GetOnGroupExpanded_Ljava_lang_Object_Handler:Com.Telerik.Widget.List.CollapsibleGroupsBehavior/ICollapseGroupListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.CollapsibleGroupsBehavior+ICollapseGroupListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", CollapsibleGroupsBehavior_CollapseGroupListenerImplementor.class, __md_methods);
	}


	public CollapsibleGroupsBehavior_CollapseGroupListenerImplementor ()
	{
		super ();
		if (getClass () == CollapsibleGroupsBehavior_CollapseGroupListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.CollapsibleGroupsBehavior+ICollapseGroupListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onGroupCollapsed (java.lang.Object p0)
	{
		n_onGroupCollapsed (p0);
	}

	private native void n_onGroupCollapsed (java.lang.Object p0);


	public void onGroupExpanded (java.lang.Object p0)
	{
		n_onGroupExpanded (p0);
	}

	private native void n_onGroupExpanded (java.lang.Object p0);

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
