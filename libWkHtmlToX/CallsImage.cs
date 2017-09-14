
using System.Runtime.InteropServices;


namespace libWkHtml2X
{


    // image.h
    public class CallsImage
    {

        private const string DLL_NAME = NativeMethods.DLL_NAME;
        private static bool? s_wkHtmlInitialized;


        static CallsImage()
        {
            // Deploy native assemblies..
            //////WkHtmlToXLibrariesManager.InitializeNativeLibrary();

            NativeMethods.Init();
            wkhtmltoimage_init(false);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltoimage_str_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.LPStr)] string str);
        //public delegate void wkhtmltoimage_str_callback(IntPtr converter, IntPtr str);
        // typedef void (* wkhtmltoimage_str_callback) (wkhtmltoimage_converter* converter, const char* str);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltoimage_int_callback(System.IntPtr converter, int val);
        // typedef void (* wkhtmltoimage_int_callback) (wkhtmltoimage_converter* converter, const int val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltoimage_bool_callback(System.IntPtr converter, bool val);
        // typedef void (* wkhtmltoimage_int_callback) (wkhtmltoimage_converter* converter, const int val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltoimage_void_callback(System.IntPtr converter);
        // typedef void (* wkhtmltoimage_void_callback) (wkhtmltoimage_converter* converter);
        
        

        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_init", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int wkhtmltoimage_init_internal(int use_graphics);
        // CAPI(int) wkhtmltoimage_init(int use_graphics);
        
        
        public static int wkhtmltoimage_init(bool use_graphics)
        {
            if (s_wkHtmlInitialized.HasValue)
                return 1;

            int ret = wkhtmltoimage_init_internal(use_graphics ? 1 : 0);
            s_wkHtmlInitialized = true;

            return ret;
        }


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_deinit", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int wkhtmltoimage_deinit_internal();
        // CAPI(int) wkhtmltoimage_deinit();

        public static int wkhtmltoimage_deinit()
        {
            System.Diagnostics.Debug.WriteLine("You cannot call wkhtmltoimage_deinit. If you do, it silently fails when you re-initialize...");
            return 1;
        }


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_extended_qt();
        // CAPI(int) wkhtmltoimage_extended_qt();


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_version", CharSet = CharSet.Unicode
            , CallingConvention = CallingConvention.StdCall)]
        private static extern System.IntPtr internal_wkhtmltoimage_version();
        // CAPI(const char*)wkhtmltoimage_version();

        public static string wkhtmltoimage_version()
        {
            System.IntPtr ptrVersion = internal_wkhtmltoimage_version();
            return ConstUtf8Marshaler._staticInstance.MarshalNativeToManaged(ptrVersion);
        }


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr wkhtmltoimage_create_global_settings();
        // CAPI(wkhtmltoimage_global_settings *) wkhtmltoimage_create_global_settings();




        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_set_global_setting"
, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltoimage_set_global_setting(System.IntPtr settings,
System.IntPtr name, System.IntPtr value);
        // CAPI(int) wkhtmltoimage_set_global_setting(wkhtmltoimage_global_settings* settings
        // , const char* name, const char* value);

        public static int wkhtmltoimage_set_global_setting(System.IntPtr settings, string name, string value)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltoimage_set_global_setting(settings, ptrName, ptrValue);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        }



        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_get_global_setting"
            , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltoimage_get_global_setting(System.IntPtr settings,
            System.IntPtr name, System.IntPtr value, int vs);
        // CAPI(int) wkhtmltoimage_get_global_setting(wkhtmltoimage_global_settings* settings, const char* name, char* value, int vs);


        public static int wkhtmltoimage_get_global_setting(System.IntPtr settings, string name, string value, int vs)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltoimage_get_global_setting(settings, ptrName, ptrValue, vs);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        } // End Function wkhtmltoimage_get_global_setting 






        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr wkhtmltoimage_create_converter(System.IntPtr globalSettings, System.IntPtr data);
        ////// CAPI(wkhtmltoimage_converter*) wkhtmltoimage_create_converter(wkhtmltoimage_global_settings* settings, const char* data);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_destroy_converter(System.IntPtr converter);
        // CAPI(void) wkhtmltoimage_destroy_converter(wkhtmltoimage_converter* converter);


        
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_warning_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltoimage_str_callback cb);
        // CAPI(void) wkhtmltoimage_set_warning_callback(wkhtmltoimage_converter* converter, wkhtmltoimage_str_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_error_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltoimage_str_callback cb);
        // CAPI(void) wkhtmltoimage_set_error_callback(wkhtmltoimage_converter* converter, wkhtmltoimage_str_callback cb);


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_phase_changed_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltoimage_void_callback cb);
        // CAPI(void) wkhtmltoimage_set_phase_changed_callback(wkhtmltoimage_converter* converter, wkhtmltoimage_void_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_progress_changed_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltoimage_int_callback cb);
        // CAPI(void) wkhtmltoimage_set_progress_changed_callback(wkhtmltoimage_converter* converter, wkhtmltoimage_int_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltoimage_set_finished_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltoimage_bool_callback cb);
        //CAPI(void) wkhtmltopdf_set_finished_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_int_callback cb);

        
        
        
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_convert(System.IntPtr converter);
        // CAPI(int) wkhtmltoimage_convert(wkhtmltoimage_converter * converter);

        
        
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_current_phase(System.IntPtr converter);
        // CAPI(int) wkhtmltoimage_current_phase(wkhtmltoimage_converter* converter);
        
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_phase_count(System.IntPtr converter);
        // CAPI(int) wkhtmltoimage_phase_count(wkhtmltoimage_converter* converter);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        // NOTE: Using IntPtr as return to avoid runtime from freeing returned string. (pruiz)
        public static extern System.IntPtr wkhtmltoimage_phase_description(System.IntPtr converter, int phase);
        // CAPI(const char*) wkhtmltoimage_phase_description(wkhtmltoimage_converter* converter, int phase);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        // NOTE: Using IntPtr as return to avoid runtime from freeing returned string. (pruiz)
        public static extern System.IntPtr wkhtmltoimage_progress_string(System.IntPtr converter);
        // CAPI(const char*) wkhtmltoimage_progress_string(wkhtmltoimage_converter* converter);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltoimage_http_error_code(System.IntPtr converter);
        // CAPI(int) wkhtmltoimage_http_error_code(wkhtmltoimage_converter* converter);



        [DllImport(DLL_NAME, EntryPoint = "wkhtmltoimage_get_output", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern System.IntPtr internal_wkhtmltoimage_get_output(System.IntPtr converter, out System.IntPtr data);
        // CAPI(long) wkhtmltoimage_get_output(wkhtmltoimage_converter* converter, const unsigned char**);

        public static byte[] wkhtmltoimage_get_output(System.IntPtr converter)
        {
            System.IntPtr data;
            System.IntPtr len = internal_wkhtmltoimage_get_output(converter, out data);

            byte[] output = new byte[len.ToInt64()];
            System.Runtime.InteropServices.Marshal.Copy(data, output, 0, output.Length);
            return output;
        } // End Function wkhtmltoimage_get_output 


    } // End Class 


} // End Namespace 
