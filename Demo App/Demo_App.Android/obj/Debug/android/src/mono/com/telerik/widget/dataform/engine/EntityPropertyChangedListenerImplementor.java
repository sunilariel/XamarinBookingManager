package mono.com.telerik.widget.dataform.engine;


public class EntityPropertyChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.EntityPropertyChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:(Lcom/telerik/widget/dataform/engine/EntityProperty;)V:GetOnChanged_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", EntityPropertyChangedListenerImplementor.class, __md_methods);
	}


	public EntityPropertyChangedListenerImplementor ()
	{
		super ();
		if (getClass () == EntityPropertyChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IEntityPropertyChangedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onChanged (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		n_onChanged (p0);
	}

	private native void n_onChanged (com.telerik.widget.dataform.engine.EntityProperty p0);

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
