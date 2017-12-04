package md5831d798aa73ef0003cbbb83504cd47e5;


public class AndroidEntityProperty
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.EntityProperty,
		com.telerik.widget.dataform.engine.PropertyChangedListener,
		com.telerik.widget.dataform.engine.ValidationCompletedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getColumnPosition:()I:GetGetColumnPositionHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setColumnPosition:(I)V:GetSetColumnPosition_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getColumnSpan:()I:GetGetColumnSpanHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setColumnSpan:(I)V:GetSetColumnSpan_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getConverter:()Lcom/telerik/widget/dataform/engine/PropertyConverter;:GetGetConverterHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setConverter:(Lcom/telerik/widget/dataform/engine/PropertyConverter;)V:GetSetConverter_Lcom_telerik_widget_dataform_engine_PropertyConverter_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getCoreEditorLayoutId:()I:GetGetCoreEditorLayoutIdHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setCoreEditorLayoutId:(I)V:GetSetCoreEditorLayoutId_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getCustomMetadata:()Ljava/lang/Object;:GetGetCustomMetadataHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setCustomMetadata:(Ljava/lang/Object;)V:GetSetCustomMetadata_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getEditorLayoutId:()I:GetGetEditorLayoutIdHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setEditorLayoutId:(I)V:GetSetEditorLayoutId_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getEditorParams:()Ljava/util/HashMap;:GetGetEditorParamsHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setEditorParams:(Ljava/util/HashMap;)V:GetSetEditorParams_Ljava_util_HashMap_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getEditorType:()Ljava/lang/Class;:GetGetEditorTypeHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setEditorType:(Ljava/lang/Class;)V:GetSetEditorType_Ljava_lang_Class_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getGroupName:()Ljava/lang/String;:GetGetGroupNameHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setGroupName:(Ljava/lang/String;)V:GetSetGroupName_Ljava_lang_String_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getHeader:()Ljava/lang/String;:GetGetHeaderHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setHeader:(Ljava/lang/String;)V:GetSetHeader_Ljava_lang_String_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getHeaderLayoutId:()I:GetGetHeaderLayoutIdHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setHeaderLayoutId:(I)V:GetSetHeaderLayoutId_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getHintText:()Ljava/lang/String;:GetGetHintTextHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setHintText:(Ljava/lang/String;)V:GetSetHintText_Ljava_lang_String_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getImageResource:()I:GetGetImageResourceHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setImageResource:(I)V:GetSetImageResource_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_isTypePrimitive:()Z:GetIsTypePrimitiveHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getOwner:()Lcom/telerik/widget/dataform/engine/Entity;:GetGetOwnerHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getPosition:()I:GetGetPositionHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setPosition:(I)V:GetSetPosition_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getReadOnly:()Z:GetGetReadOnlyHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setReadOnly:(Z)V:GetSetReadOnly_ZHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getRequired:()Z:GetGetRequiredHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setRequired:(Z)V:GetSetRequired_ZHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getSkip:()Z:GetGetSkipHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setSkip:(Z)V:GetSetSkip_ZHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getValidationLayoutId:()I:GetGetValidationLayoutIdHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setValidationLayoutId:(I)V:GetSetValidationLayoutId_IHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getValidator:()Lcom/telerik/widget/dataform/engine/PropertyValidator;:GetGetValidatorHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setValidator:(Lcom/telerik/widget/dataform/engine/PropertyValidator;)V:GetSetValidator_Lcom_telerik_widget_dataform_engine_PropertyValidator_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getValue:()Ljava/lang/Object;:GetGetValueHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getValueCandidate:()Ljava/lang/Object;:GetGetValueCandidateHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setValueCandidate:(Ljava/lang/Object;)V:GetSetValueCandidate_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getViewerType:()Ljava/lang/Class;:GetGetViewerTypeHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_setViewerType:(Ljava/lang/Class;)V:GetSetViewerType_Ljava_lang_Class_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_addCommitListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyCommitListener;)V:GetAddCommitListener_Lcom_telerik_widget_dataform_engine_EntityPropertyCommitListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_addOnChangedListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyChangedListener;)V:GetAddOnChangedListener_Lcom_telerik_widget_dataform_engine_EntityPropertyChangedListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_addValidationCompletedListener:(Lcom/telerik/widget/dataform/engine/ValidationCompletedListener;)V:GetAddValidationCompletedListener_Lcom_telerik_widget_dataform_engine_ValidationCompletedListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_commit:()V:GetCommitHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_getEnumConstants:()[Ljava/lang/Object;:GetGetEnumConstantsHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_name:()Ljava/lang/String;:GetNameHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_readMetadata:(Lcom/telerik/widget/dataform/engine/EntityPropertyMetadata;)V:GetReadMetadata_Lcom_telerik_widget_dataform_engine_EntityPropertyMetadata_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_removeCommitListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyCommitListener;)V:GetRemoveCommitListener_Lcom_telerik_widget_dataform_engine_EntityPropertyCommitListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_removeOnChangedListener:(Lcom/telerik/widget/dataform/engine/EntityPropertyChangedListener;)V:GetRemoveOnChangedListener_Lcom_telerik_widget_dataform_engine_EntityPropertyChangedListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_removeValidationCompletedListener:(Lcom/telerik/widget/dataform/engine/ValidationCompletedListener;)V:GetRemoveValidationCompletedListener_Lcom_telerik_widget_dataform_engine_ValidationCompletedListener_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_tryCommit:(Ljava/lang/Object;)V:GetTryCommit_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_type:()Ljava/lang/Class;:GetTypeHandler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_validate:(Ljava/lang/Object;)V:GetValidate_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IEntityPropertyInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_onPropertyChanged:(Ljava/lang/String;Ljava/lang/Object;)V:GetOnPropertyChanged_Ljava_lang_String_Ljava_lang_Object_Handler:Com.Telerik.Widget.Dataform.Engine.IPropertyChangedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"n_validationCompleted:(Lcom/telerik/widget/dataform/engine/ValidationInfo;)V:GetValidationCompleted_Lcom_telerik_widget_dataform_engine_ValidationInfo_Handler:Com.Telerik.Widget.Dataform.Engine.IValidationCompletedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.DataForm.AndroidEntityProperty, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", AndroidEntityProperty.class, __md_methods);
	}


	public AndroidEntityProperty ()
	{
		super ();
		if (getClass () == AndroidEntityProperty.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.DataForm.AndroidEntityProperty, Telerik.XamarinForms.Input, Version=2017.3.1018.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getColumnPosition ()
	{
		return n_getColumnPosition ();
	}

	private native int n_getColumnPosition ();


	public void setColumnPosition (int p0)
	{
		n_setColumnPosition (p0);
	}

	private native void n_setColumnPosition (int p0);


	public int getColumnSpan ()
	{
		return n_getColumnSpan ();
	}

	private native int n_getColumnSpan ();


	public void setColumnSpan (int p0)
	{
		n_setColumnSpan (p0);
	}

	private native void n_setColumnSpan (int p0);


	public com.telerik.widget.dataform.engine.PropertyConverter getConverter ()
	{
		return n_getConverter ();
	}

	private native com.telerik.widget.dataform.engine.PropertyConverter n_getConverter ();


	public void setConverter (com.telerik.widget.dataform.engine.PropertyConverter p0)
	{
		n_setConverter (p0);
	}

	private native void n_setConverter (com.telerik.widget.dataform.engine.PropertyConverter p0);


	public int getCoreEditorLayoutId ()
	{
		return n_getCoreEditorLayoutId ();
	}

	private native int n_getCoreEditorLayoutId ();


	public void setCoreEditorLayoutId (int p0)
	{
		n_setCoreEditorLayoutId (p0);
	}

	private native void n_setCoreEditorLayoutId (int p0);


	public java.lang.Object getCustomMetadata ()
	{
		return n_getCustomMetadata ();
	}

	private native java.lang.Object n_getCustomMetadata ();


	public void setCustomMetadata (java.lang.Object p0)
	{
		n_setCustomMetadata (p0);
	}

	private native void n_setCustomMetadata (java.lang.Object p0);


	public int getEditorLayoutId ()
	{
		return n_getEditorLayoutId ();
	}

	private native int n_getEditorLayoutId ();


	public void setEditorLayoutId (int p0)
	{
		n_setEditorLayoutId (p0);
	}

	private native void n_setEditorLayoutId (int p0);


	public java.util.HashMap getEditorParams ()
	{
		return n_getEditorParams ();
	}

	private native java.util.HashMap n_getEditorParams ();


	public void setEditorParams (java.util.HashMap p0)
	{
		n_setEditorParams (p0);
	}

	private native void n_setEditorParams (java.util.HashMap p0);


	public java.lang.Class getEditorType ()
	{
		return n_getEditorType ();
	}

	private native java.lang.Class n_getEditorType ();


	public void setEditorType (java.lang.Class p0)
	{
		n_setEditorType (p0);
	}

	private native void n_setEditorType (java.lang.Class p0);


	public java.lang.String getGroupName ()
	{
		return n_getGroupName ();
	}

	private native java.lang.String n_getGroupName ();


	public void setGroupName (java.lang.String p0)
	{
		n_setGroupName (p0);
	}

	private native void n_setGroupName (java.lang.String p0);


	public java.lang.String getHeader ()
	{
		return n_getHeader ();
	}

	private native java.lang.String n_getHeader ();


	public void setHeader (java.lang.String p0)
	{
		n_setHeader (p0);
	}

	private native void n_setHeader (java.lang.String p0);


	public int getHeaderLayoutId ()
	{
		return n_getHeaderLayoutId ();
	}

	private native int n_getHeaderLayoutId ();


	public void setHeaderLayoutId (int p0)
	{
		n_setHeaderLayoutId (p0);
	}

	private native void n_setHeaderLayoutId (int p0);


	public java.lang.String getHintText ()
	{
		return n_getHintText ();
	}

	private native java.lang.String n_getHintText ();


	public void setHintText (java.lang.String p0)
	{
		n_setHintText (p0);
	}

	private native void n_setHintText (java.lang.String p0);


	public int getImageResource ()
	{
		return n_getImageResource ();
	}

	private native int n_getImageResource ();


	public void setImageResource (int p0)
	{
		n_setImageResource (p0);
	}

	private native void n_setImageResource (int p0);


	public boolean isTypePrimitive ()
	{
		return n_isTypePrimitive ();
	}

	private native boolean n_isTypePrimitive ();


	public com.telerik.widget.dataform.engine.Entity getOwner ()
	{
		return n_getOwner ();
	}

	private native com.telerik.widget.dataform.engine.Entity n_getOwner ();


	public int getPosition ()
	{
		return n_getPosition ();
	}

	private native int n_getPosition ();


	public void setPosition (int p0)
	{
		n_setPosition (p0);
	}

	private native void n_setPosition (int p0);


	public boolean getReadOnly ()
	{
		return n_getReadOnly ();
	}

	private native boolean n_getReadOnly ();


	public void setReadOnly (boolean p0)
	{
		n_setReadOnly (p0);
	}

	private native void n_setReadOnly (boolean p0);


	public boolean getRequired ()
	{
		return n_getRequired ();
	}

	private native boolean n_getRequired ();


	public void setRequired (boolean p0)
	{
		n_setRequired (p0);
	}

	private native void n_setRequired (boolean p0);


	public boolean getSkip ()
	{
		return n_getSkip ();
	}

	private native boolean n_getSkip ();


	public void setSkip (boolean p0)
	{
		n_setSkip (p0);
	}

	private native void n_setSkip (boolean p0);


	public int getValidationLayoutId ()
	{
		return n_getValidationLayoutId ();
	}

	private native int n_getValidationLayoutId ();


	public void setValidationLayoutId (int p0)
	{
		n_setValidationLayoutId (p0);
	}

	private native void n_setValidationLayoutId (int p0);


	public com.telerik.widget.dataform.engine.PropertyValidator getValidator ()
	{
		return n_getValidator ();
	}

	private native com.telerik.widget.dataform.engine.PropertyValidator n_getValidator ();


	public void setValidator (com.telerik.widget.dataform.engine.PropertyValidator p0)
	{
		n_setValidator (p0);
	}

	private native void n_setValidator (com.telerik.widget.dataform.engine.PropertyValidator p0);


	public java.lang.Object getValue ()
	{
		return n_getValue ();
	}

	private native java.lang.Object n_getValue ();


	public java.lang.Object getValueCandidate ()
	{
		return n_getValueCandidate ();
	}

	private native java.lang.Object n_getValueCandidate ();


	public void setValueCandidate (java.lang.Object p0)
	{
		n_setValueCandidate (p0);
	}

	private native void n_setValueCandidate (java.lang.Object p0);


	public java.lang.Class getViewerType ()
	{
		return n_getViewerType ();
	}

	private native java.lang.Class n_getViewerType ();


	public void setViewerType (java.lang.Class p0)
	{
		n_setViewerType (p0);
	}

	private native void n_setViewerType (java.lang.Class p0);


	public void addCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0)
	{
		n_addCommitListener (p0);
	}

	private native void n_addCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0);


	public void addOnChangedListener (com.telerik.widget.dataform.engine.EntityPropertyChangedListener p0)
	{
		n_addOnChangedListener (p0);
	}

	private native void n_addOnChangedListener (com.telerik.widget.dataform.engine.EntityPropertyChangedListener p0);


	public void addValidationCompletedListener (com.telerik.widget.dataform.engine.ValidationCompletedListener p0)
	{
		n_addValidationCompletedListener (p0);
	}

	private native void n_addValidationCompletedListener (com.telerik.widget.dataform.engine.ValidationCompletedListener p0);


	public void commit ()
	{
		n_commit ();
	}

	private native void n_commit ();


	public java.lang.Object[] getEnumConstants ()
	{
		return n_getEnumConstants ();
	}

	private native java.lang.Object[] n_getEnumConstants ();


	public java.lang.String name ()
	{
		return n_name ();
	}

	private native java.lang.String n_name ();


	public void readMetadata (com.telerik.widget.dataform.engine.EntityPropertyMetadata p0)
	{
		n_readMetadata (p0);
	}

	private native void n_readMetadata (com.telerik.widget.dataform.engine.EntityPropertyMetadata p0);


	public void removeCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0)
	{
		n_removeCommitListener (p0);
	}

	private native void n_removeCommitListener (com.telerik.widget.dataform.engine.EntityPropertyCommitListener p0);


	public void removeOnChangedListener (com.telerik.widget.dataform.engine.EntityPropertyChangedListener p0)
	{
		n_removeOnChangedListener (p0);
	}

	private native void n_removeOnChangedListener (com.telerik.widget.dataform.engine.EntityPropertyChangedListener p0);


	public void removeValidationCompletedListener (com.telerik.widget.dataform.engine.ValidationCompletedListener p0)
	{
		n_removeValidationCompletedListener (p0);
	}

	private native void n_removeValidationCompletedListener (com.telerik.widget.dataform.engine.ValidationCompletedListener p0);


	public void tryCommit (java.lang.Object p0)
	{
		n_tryCommit (p0);
	}

	private native void n_tryCommit (java.lang.Object p0);


	public java.lang.Class type ()
	{
		return n_type ();
	}

	private native java.lang.Class n_type ();


	public void validate (java.lang.Object p0)
	{
		n_validate (p0);
	}

	private native void n_validate (java.lang.Object p0);


	public void onPropertyChanged (java.lang.String p0, java.lang.Object p1)
	{
		n_onPropertyChanged (p0, p1);
	}

	private native void n_onPropertyChanged (java.lang.String p0, java.lang.Object p1);


	public void validationCompleted (com.telerik.widget.dataform.engine.ValidationInfo p0)
	{
		n_validationCompleted (p0);
	}

	private native void n_validationCompleted (com.telerik.widget.dataform.engine.ValidationInfo p0);

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
