package mono.com.telerik.android.primitives.widget.tabview;


public class TabViewChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.android.primitives.widget.tabview.TabViewChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getContentViewForTab:(Lcom/telerik/android/primitives/widget/tabstrip/Tab;)Landroid/view/View;:GetGetContentViewForTab_Lcom_telerik_android_primitives_widget_tabstrip_Tab_Handler:Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_getViewForTab:(Lcom/telerik/android/primitives/widget/tabstrip/Tab;)Lcom/telerik/android/primitives/widget/tabstrip/TabItemView;:GetGetViewForTab_Lcom_telerik_android_primitives_widget_tabstrip_Tab_Handler:Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_onSelectingTab:(Lcom/telerik/android/primitives/widget/tabstrip/Tab;)Z:GetOnSelectingTab_Lcom_telerik_android_primitives_widget_tabstrip_Tab_Handler:Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"n_onTabSelected:(Lcom/telerik/android/primitives/widget/tabstrip/Tab;Lcom/telerik/android/primitives/widget/tabstrip/Tab;)V:GetOnTabSelected_Lcom_telerik_android_primitives_widget_tabstrip_Tab_Lcom_telerik_android_primitives_widget_tabstrip_Tab_Handler:Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerInvoker, Telerik.Xamarin.Android.Primitives\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", TabViewChangeListenerImplementor.class, __md_methods);
	}


	public TabViewChangeListenerImplementor ()
	{
		super ();
		if (getClass () == TabViewChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Android.Primitives.Widget.Tabview.ITabViewChangeListenerImplementor, Telerik.Xamarin.Android.Primitives, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.view.View getContentViewForTab (com.telerik.android.primitives.widget.tabstrip.Tab p0)
	{
		return n_getContentViewForTab (p0);
	}

	private native android.view.View n_getContentViewForTab (com.telerik.android.primitives.widget.tabstrip.Tab p0);


	public com.telerik.android.primitives.widget.tabstrip.TabItemView getViewForTab (com.telerik.android.primitives.widget.tabstrip.Tab p0)
	{
		return n_getViewForTab (p0);
	}

	private native com.telerik.android.primitives.widget.tabstrip.TabItemView n_getViewForTab (com.telerik.android.primitives.widget.tabstrip.Tab p0);


	public boolean onSelectingTab (com.telerik.android.primitives.widget.tabstrip.Tab p0)
	{
		return n_onSelectingTab (p0);
	}

	private native boolean n_onSelectingTab (com.telerik.android.primitives.widget.tabstrip.Tab p0);


	public void onTabSelected (com.telerik.android.primitives.widget.tabstrip.Tab p0, com.telerik.android.primitives.widget.tabstrip.Tab p1)
	{
		n_onTabSelected (p0, p1);
	}

	private native void n_onTabSelected (com.telerik.android.primitives.widget.tabstrip.Tab p0, com.telerik.android.primitives.widget.tabstrip.Tab p1);

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
