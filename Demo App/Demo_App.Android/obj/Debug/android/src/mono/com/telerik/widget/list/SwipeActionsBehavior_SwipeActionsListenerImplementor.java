package mono.com.telerik.widget.list;


public class SwipeActionsBehavior_SwipeActionsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.list.SwipeActionsBehavior.SwipeActionsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onExecuteFinished:(Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionEvent;)V:GetOnExecuteFinished_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionEvent_Handler:Com.Telerik.Widget.List.SwipeActionsBehavior/ISwipeActionsListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onSwipeEnded:(Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionEvent;)V:GetOnSwipeEnded_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionEvent_Handler:Com.Telerik.Widget.List.SwipeActionsBehavior/ISwipeActionsListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onSwipeProgressChanged:(Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionEvent;)V:GetOnSwipeProgressChanged_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionEvent_Handler:Com.Telerik.Widget.List.SwipeActionsBehavior/ISwipeActionsListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onSwipeStarted:(Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionEvent;)V:GetOnSwipeStarted_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionEvent_Handler:Com.Telerik.Widget.List.SwipeActionsBehavior/ISwipeActionsListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"n_onSwipeStateChanged:(Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionsState;Lcom/telerik/widget/list/SwipeActionsBehavior$SwipeActionsState;)V:GetOnSwipeStateChanged_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionsState_Lcom_telerik_widget_list_SwipeActionsBehavior_SwipeActionsState_Handler:Com.Telerik.Widget.List.SwipeActionsBehavior/ISwipeActionsListenerInvoker, Telerik.Xamarin.Android.List\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.List.SwipeActionsBehavior+ISwipeActionsListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", SwipeActionsBehavior_SwipeActionsListenerImplementor.class, __md_methods);
	}


	public SwipeActionsBehavior_SwipeActionsListenerImplementor ()
	{
		super ();
		if (getClass () == SwipeActionsBehavior_SwipeActionsListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.List.SwipeActionsBehavior+ISwipeActionsListenerImplementor, Telerik.Xamarin.Android.List, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onExecuteFinished (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0)
	{
		n_onExecuteFinished (p0);
	}

	private native void n_onExecuteFinished (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0);


	public void onSwipeEnded (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0)
	{
		n_onSwipeEnded (p0);
	}

	private native void n_onSwipeEnded (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0);


	public void onSwipeProgressChanged (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0)
	{
		n_onSwipeProgressChanged (p0);
	}

	private native void n_onSwipeProgressChanged (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0);


	public void onSwipeStarted (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0)
	{
		n_onSwipeStarted (p0);
	}

	private native void n_onSwipeStarted (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionEvent p0);


	public void onSwipeStateChanged (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionsState p0, com.telerik.widget.list.SwipeActionsBehavior.SwipeActionsState p1)
	{
		n_onSwipeStateChanged (p0, p1);
	}

	private native void n_onSwipeStateChanged (com.telerik.widget.list.SwipeActionsBehavior.SwipeActionsState p0, com.telerik.widget.list.SwipeActionsBehavior.SwipeActionsState p1);

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
