package mono.com.telerik.widget.dataform.engine;


public class EditorSetupListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.EditorSetupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEditorSetupFinished:(Lcom/telerik/widget/dataform/visualization/core/EntityPropertyViewer;)V:GetOnEditorSetupFinished_Lcom_telerik_widget_dataform_visualization_core_EntityPropertyViewer_Handler:Com.Telerik.Widget.Dataform.Engine.IEditorSetupListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_onEditorSetupStarted:(Lcom/telerik/widget/dataform/visualization/core/EntityPropertyViewer;)V:GetOnEditorSetupStarted_Lcom_telerik_widget_dataform_visualization_core_EntityPropertyViewer_Handler:Com.Telerik.Widget.Dataform.Engine.IEditorSetupListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IEditorSetupListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", EditorSetupListenerImplementor.class, __md_methods);
	}


	public EditorSetupListenerImplementor ()
	{
		super ();
		if (getClass () == EditorSetupListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IEditorSetupListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onEditorSetupFinished (com.telerik.widget.dataform.visualization.core.EntityPropertyViewer p0)
	{
		n_onEditorSetupFinished (p0);
	}

	private native void n_onEditorSetupFinished (com.telerik.widget.dataform.visualization.core.EntityPropertyViewer p0);


	public void onEditorSetupStarted (com.telerik.widget.dataform.visualization.core.EntityPropertyViewer p0)
	{
		n_onEditorSetupStarted (p0);
	}

	private native void n_onEditorSetupStarted (com.telerik.widget.dataform.visualization.core.EntityPropertyViewer p0);

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
