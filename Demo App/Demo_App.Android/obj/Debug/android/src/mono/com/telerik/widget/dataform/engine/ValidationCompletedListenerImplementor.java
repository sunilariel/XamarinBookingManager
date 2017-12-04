package mono.com.telerik.widget.dataform.engine;


public class ValidationCompletedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.dataform.engine.ValidationCompletedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_validationCompleted:(Lcom/telerik/widget/dataform/engine/ValidationInfo;)V:GetValidationCompleted_Lcom_telerik_widget_dataform_engine_ValidationInfo_Handler:Com.Telerik.Widget.Dataform.Engine.IValidationCompletedListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Com.Telerik.Widget.Dataform.Engine.IValidationCompletedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", ValidationCompletedListenerImplementor.class, __md_methods);
	}


	public ValidationCompletedListenerImplementor ()
	{
		super ();
		if (getClass () == ValidationCompletedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Telerik.Widget.Dataform.Engine.IValidationCompletedListenerImplementor, Telerik.Xamarin.Android.Input, Version=2017.3.1018.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


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
