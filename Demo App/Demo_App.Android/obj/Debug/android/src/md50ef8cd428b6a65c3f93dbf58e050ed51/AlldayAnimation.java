package md50ef8cd428b6a65c3f93dbf58e050ed51;


public class AlldayAnimation
	extends android.view.animation.Animation
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_applyTransformation:(FLandroid/view/animation/Transformation;)V:GetApplyTransformation_FLandroid_view_animation_Transformation_Handler\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Schedule.AlldayAnimation, Syncfusion.SfSchedule.Android, Version=16.2451.0.41, Culture=neutral, PublicKeyToken=null", AlldayAnimation.class, __md_methods);
	}


	public AlldayAnimation ()
	{
		super ();
		if (getClass () == AlldayAnimation.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Schedule.AlldayAnimation, Syncfusion.SfSchedule.Android, Version=16.2451.0.41, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public AlldayAnimation (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == AlldayAnimation.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Schedule.AlldayAnimation, Syncfusion.SfSchedule.Android, Version=16.2451.0.41, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

	public AlldayAnimation (android.view.View p0, int p1, int p2)
	{
		super ();
		if (getClass () == AlldayAnimation.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Schedule.AlldayAnimation, Syncfusion.SfSchedule.Android, Version=16.2451.0.41, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void applyTransformation (float p0, android.view.animation.Transformation p1)
	{
		n_applyTransformation (p0, p1);
	}

	private native void n_applyTransformation (float p0, android.view.animation.Transformation p1);

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
