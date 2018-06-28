package mono.com.telerik.widget.dataform.visualization;


public class ExpandableEditorGroup_IsExpandedChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.visualization.ExpandableEditorGroup.IsExpandedChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:(Z)V:GetOnChanged_ZHandler:Com.Telerik.Widget.Dataform.Visualization.ExpandableEditorGroup/IIsExpandedChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Visualization.ExpandableEditorGroup+IIsExpandedChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", ExpandableEditorGroup_IsExpandedChangedListenerImplementor.class, __md_methods);
	}


	public ExpandableEditorGroup_IsExpandedChangedListenerImplementor ()
	{
		super ();
		if (getClass () == ExpandableEditorGroup_IsExpandedChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Visualization.ExpandableEditorGroup+IIsExpandedChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
