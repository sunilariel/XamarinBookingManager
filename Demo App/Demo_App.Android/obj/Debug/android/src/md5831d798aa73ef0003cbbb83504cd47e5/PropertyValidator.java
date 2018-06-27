package md5831d798aa73ef0003cbbb83504cd47e5;


public class PropertyValidator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.PropertyValidator
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getNegativeMessage:()Ljava/lang/String;:GetGetNegativeMessageHandler:Com.Telerik.Widget.Dataform.Engine.IPropertyValidatorInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setNegativeMessage:(Ljava/lang/String;)V:GetSetNegativeMessage_Ljava_lang_String_Handler:Com.Telerik.Widget.Dataform.Engine.IPropertyValidatorInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getPositiveMessage:()Ljava/lang/String;:GetGetPositiveMessageHandler:Com.Telerik.Widget.Dataform.Engine.IPropertyValidatorInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setPositiveMessage:(Ljava/lang/String;)V:GetSetPositiveMessage_Ljava_lang_String_Handler:Com.Telerik.Widget.Dataform.Engine.IPropertyValidatorInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_validate:(Ljava/lang/Object;Ljava/lang/String;Lcom/telerik/widget/dataform/engine/ValidationCompletedListener;)V:GetValidate_Ljava_lang_Object_Ljava_lang_String_Lcom_telerik_widget_dataform_engine_ValidationCompletedListener_Handler:Com.Telerik.Widget.Dataform.Engine.IPropertyValidatorInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.DataForm.PropertyValidator, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", PropertyValidator.class, __md_methods);
	}


	public PropertyValidator ()
	{
		super ();
		if (getClass () == PropertyValidator.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.DataForm.PropertyValidator, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String getNegativeMessage ()
	{
		return n_getNegativeMessage ();
	}

	private native java.lang.String n_getNegativeMessage ();


	public void setNegativeMessage (java.lang.String p0)
	{
		n_setNegativeMessage (p0);
	}

	private native void n_setNegativeMessage (java.lang.String p0);


	public java.lang.String getPositiveMessage ()
	{
		return n_getPositiveMessage ();
	}

	private native java.lang.String n_getPositiveMessage ();


	public void setPositiveMessage (java.lang.String p0)
	{
		n_setPositiveMessage (p0);
	}

	private native void n_setPositiveMessage (java.lang.String p0);


	public void validate (java.lang.Object p0, java.lang.String p1, com.telerik.widget.dataform.engine.ValidationCompletedListener p2)
	{
		n_validate (p0, p1, p2);
	}

	private native void n_validate (java.lang.Object p0, java.lang.String p1, com.telerik.widget.dataform.engine.ValidationCompletedListener p2);

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
