package mono.com.telerik.widget.dataform.engine;


public class EntityPropertyCommitListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.EntityPropertyCommitListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAfterCommit:(Lcom/telerik/widget/dataform/engine/EntityProperty;)V:GetOnAfterCommit_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyCommitListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_onBeforeCommit:(Lcom/telerik/widget/dataform/engine/EntityProperty;)Z:GetOnBeforeCommit_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyCommitListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyCommitListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", EntityPropertyCommitListenerImplementor.class, __md_methods);
	}


	public EntityPropertyCommitListenerImplementor ()
	{
		super ();
		if (getClass () == EntityPropertyCommitListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyCommitListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onAfterCommit (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		n_onAfterCommit (p0);
	}

	private native void n_onAfterCommit (com.telerik.widget.dataform.engine.EntityProperty p0);


	public boolean onBeforeCommit (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		return n_onBeforeCommit (p0);
	}

	private native boolean n_onBeforeCommit (com.telerik.widget.dataform.engine.EntityProperty p0);

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
