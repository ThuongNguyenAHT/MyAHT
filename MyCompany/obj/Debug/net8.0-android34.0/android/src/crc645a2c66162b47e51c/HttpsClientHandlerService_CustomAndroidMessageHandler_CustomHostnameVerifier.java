package crc645a2c66162b47e51c;


public class HttpsClientHandlerService_CustomAndroidMessageHandler_CustomHostnameVerifier
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		javax.net.ssl.HostnameVerifier
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_verify:(Ljava/lang/String;Ljavax/net/ssl/SSLSession;)Z:GetVerify_Ljava_lang_String_Ljavax_net_ssl_SSLSession_Handler:Javax.Net.Ssl.IHostnameVerifierInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("MyCompany.Model.HttpsClientHandlerService+CustomAndroidMessageHandler+CustomHostnameVerifier, MyCompany", HttpsClientHandlerService_CustomAndroidMessageHandler_CustomHostnameVerifier.class, __md_methods);
	}


	public HttpsClientHandlerService_CustomAndroidMessageHandler_CustomHostnameVerifier ()
	{
		super ();
		if (getClass () == HttpsClientHandlerService_CustomAndroidMessageHandler_CustomHostnameVerifier.class) {
			mono.android.TypeManager.Activate ("MyCompany.Model.HttpsClientHandlerService+CustomAndroidMessageHandler+CustomHostnameVerifier, MyCompany", "", this, new java.lang.Object[] {  });
		}
	}


	public boolean verify (java.lang.String p0, javax.net.ssl.SSLSession p1)
	{
		return n_verify (p0, p1);
	}

	private native boolean n_verify (java.lang.String p0, javax.net.ssl.SSLSession p1);

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
