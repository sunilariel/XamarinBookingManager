package md5831d798aa73ef0003cbbb83504cd47e5;


public class AndroidEntity
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.Entity
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getSourceObject:()Ljava/lang/Object;:GetGetSourceObjectHandler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_addCommitListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyCommitListener;)V:GetAddCommitListener_Lcom_telerik_widget_dataform_engine_EntityPropertyCommitListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getProperty:(Lcom/telerik/widget/dataform/engine/EntityProperty;)Ljava/lang/Object;:GetGetProperty_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_notifyCommitListenersAfter:(Lcom/telerik/widget/dataform/engine/EntityProperty;)V:GetNotifyCommitListenersAfter_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_notifyCommitListenersBefore:(Lcom/telerik/widget/dataform/engine/EntityProperty;)Z:GetNotifyCommitListenersBefore_Lcom_telerik_widget_dataform_engine_EntityProperty_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_properties:()Ljava/lang/Iterable;:GetPropertiesHandler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_removeCommitListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyCommitListener;)V:GetRemoveCommitListener_Lcom_telerik_widget_dataform_engine_EntityPropertyCommitListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setProperty:(Lcom/telerik/widget/dataform/engine/EntityProperty;Ljava/lang/Object;)V:GetSetProperty_Lcom_telerik_widget_dataform_engine_EntityProperty_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.DataForm.AndroidEntity, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEntity.class, __md_methods);
	}


	public AndroidEntity ()
	{
		super ();
		if (getClass () == AndroidEntity.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.DataForm.AndroidEntity, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object getSourceObject ()
	{
		return n_getSourceObject ();
	}

	private native java.lang.Object n_getSourceObject ();


	public void addCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0)
	{
		n_addCommitListener (p0);
	}

	private native void n_addCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0);


	public java.lang.Object getProperty (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		return n_getProperty (p0);
	}

	private native java.lang.Object n_getProperty (com.telerik.widget.dataform.engine.EntityProperty p0);


	public void notifyCommitListenersAfter (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		n_notifyCommitListenersAfter (p0);
	}

	private native void n_notifyCommitListenersAfter (com.telerik.widget.dataform.engine.EntityProperty p0);


	public boolean notifyCommitListenersBefore (com.telerik.widget.dataform.engine.EntityProperty p0)
	{
		return n_notifyCommitListenersBefore (p0);
	}

	private native boolean n_notifyCommitListenersBefore (com.telerik.widget.dataform.engine.EntityProperty p0);


	public java.lang.Iterable properties ()
	{
		return n_properties ();
	}

	private native java.lang.Iterable n_properties ();


	public void removeCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0)
	{
		n_removeCommitListener (p0);
	}

	private native void n_removeCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0);


	public void setProperty (com.telerik.widget.dataform.engine.EntityProperty p0, java.lang.Object p1)
	{
		n_setProperty (p0, p1);
	}

	private native void n_setProperty (com.telerik.widget.dataform.engine.EntityProperty p0, java.lang.Object p1);

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
