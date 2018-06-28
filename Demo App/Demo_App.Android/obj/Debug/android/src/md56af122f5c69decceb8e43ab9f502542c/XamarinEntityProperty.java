package md56af122f5c69decceb8e43ab9f502542c;


public class XamarinEntityProperty
	extends com.telerik.widget.dataform.engine.EntityPropertyCore
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_readMetadata:(Ljava/lang/String;Lcom/telerik/widget/dataform/engine/Entity;)V:GetReadMetadata_Ljava_lang_String_Lcom_telerik_widget_dataform_engine_Entity_Handler\n" +
			"n_getReadOnly:()Z:GetGetReadOnlyHandler\n" +
			"n_name:()Ljava/lang/String;:GetNameHandler\n" +
			"n_getEnumConstants:()[Ljava/lang/Object;:GetGetEnumConstantsHandler\n" +
			"n_isTypePrimitive:()Z:GetIsTypePrimitiveHandler\n" +
			"n_type:()Ljava/lang/Class;:GetTypeHandler\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.XamarinEntityProperty, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", XamarinEntityProperty.class, __md_methods);
	}


	public XamarinEntityProperty (java.lang.String p0, java.lang.Class p1, com.telerik.widget.dataform.engine.Entity p2)
	{
		super (p0, p1, p2);
		if (getClass () == XamarinEntityProperty.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.XamarinEntityProperty, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Java.Lang.Class, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Com.Telerik.Widget.Dataform.Engine.IEntity, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void readMetadata (java.lang.String p0, com.telerik.widget.dataform.engine.Entity p1)
	{
		n_readMetadata (p0, p1);
	}

	private native void n_readMetadata (java.lang.String p0, com.telerik.widget.dataform.engine.Entity p1);


	public boolean getReadOnly ()
	{
		return n_getReadOnly ();
	}

	private native boolean n_getReadOnly ();


	public java.lang.String name ()
	{
		return n_name ();
	}

	private native java.lang.String n_name ();


	public java.lang.Object[] getEnumConstants ()
	{
		return n_getEnumConstants ();
	}

	private native java.lang.Object[] n_getEnumConstants ();


	public boolean isTypePrimitive ()
	{
		return n_isTypePrimitive ();
	}

	private native boolean n_isTypePrimitive ();


	public java.lang.Class type ()
	{
		return n_type ();
	}

	private native java.lang.Class n_type ();

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
