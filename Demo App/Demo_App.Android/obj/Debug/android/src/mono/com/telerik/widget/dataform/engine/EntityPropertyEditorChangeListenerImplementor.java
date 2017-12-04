package mono.com.telerik.widget.dataform.engine;


public class EntityPropertyEditorChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.EntityPropertyEditorChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEditorChanged:(Lcom/telerik/widget/dataform/visualization/core/EntityPropertyEditor;)V:GetOnEditorChanged_Lcom_telerik_widget_dataform_visualization_core_EntityPropertyEditor_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyEditorChangeListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyEditorChangeListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", EntityPropertyEditorChangeListenerImplementor.class, __md_methods);
	}


	public EntityPropertyEditorChangeListenerImplementor ()
	{
		super ();
		if (getClass () == EntityPropertyEditorChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyEditorChangeListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onEditorChanged (com.telerik.widget.dataform.visualization.core.EntityPropertyEditor p0)
	{
		n_onEditorChanged (p0);
	}

	private native void n_onEditorChanged (com.telerik.widget.dataform.visualization.core.EntityPropertyEditor p0);

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
