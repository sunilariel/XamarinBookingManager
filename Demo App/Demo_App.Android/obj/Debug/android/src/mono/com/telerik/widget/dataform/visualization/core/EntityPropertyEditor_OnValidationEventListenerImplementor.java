package mono.com.telerik.widget.dataform.visualization.core;


public class EntityPropertyEditor_OnValidationEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.visualization.core.EntityPropertyEditor.OnValidationEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onValidationEvent:(Lcom/telerik/widget/dataform/visualization/core/EntityPropertyEditor;Lcom/telerik/widget/dataform/engine/ValidationInfo;)V:GetOnValidationEvent_Lcom_telerik_widget_dataform_visualization_core_EntityPropertyEditor_Lcom_telerik_widget_dataform_engine_ValidationInfo_Handler:Com.Telerik.Widget.Dataform.Visualization.Core.EntityPropertyEditor/IOnValidationEventListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Visualization.Core.EntityPropertyEditor+IOnValidationEventListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", EntityPropertyEditor_OnValidationEventListenerImplementor.class, __md_methods);
	}


	public EntityPropertyEditor_OnValidationEventListenerImplementor ()
	{
		super ();
		if (getClass () == EntityPropertyEditor_OnValidationEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Visualization.Core.EntityPropertyEditor+IOnValidationEventListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onValidationEvent (com.telerik.widget.dataform.visualization.core.EntityPropertyEditor p0, com.telerik.widget.dataform.engine.ValidationInfo p1)
	{
		n_onValidationEvent (p0, p1);
	}

	private native void n_onValidationEvent (com.telerik.widget.dataform.visualization.core.EntityPropertyEditor p0, com.telerik.widget.dataform.engine.ValidationInfo p1);

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
