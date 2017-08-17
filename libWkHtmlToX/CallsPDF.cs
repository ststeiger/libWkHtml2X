
using System.Runtime.InteropServices;


namespace libWkHtml2X
{

    // pdf.h
    public static class CallsPDF
    {
        private const string DLL_NAME = NativeMethods.DLL_NAME;


        static CallsPDF()
        {
            // Deploy native assemblies..
            //////WkHtmlToXLibrariesManager.InitializeNativeLibrary();

            NativeMethods.Init();
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltopdf_str_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.LPStr)] string str);
        //public delegate void wkhtmltopdf_str_callback(IntPtr converter, IntPtr str);
        // typedef void (* wkhtmltopdf_str_callback) (wkhtmltopdf_converter* converter, const char* str);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltopdf_int_callback(System.IntPtr converter, int val);
        // typedef void (* wkhtmltopdf_int_callback) (wkhtmltopdf_converter* converter, const int val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltopdf_bool_callback(System.IntPtr converter, bool val);
        // typedef void (* wkhtmltopdf_int_callback) (wkhtmltopdf_converter* converter, const int val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void wkhtmltopdf_void_callback(System.IntPtr converter);
        // typedef void (* wkhtmltopdf_void_callback) (wkhtmltopdf_converter* converter);



        

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_init(int use_graphics);
        // CAPI(int) wkhtmltopdf_init(int use_graphics);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_deinit();
        // CAPI(int) wkhtmltopdf_deinit(); 

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_extended_qt();
        // CAPI(int) wkhtmltopdf_extended_qt();


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_version", CharSet = CharSet.Unicode
            , CallingConvention = CallingConvention.StdCall)]
        private static extern System.IntPtr internal_wkhtmltopdf_version();
        // CAPI(const char *) wkhtmltopdf_version();

        public static string wkhtmltopdf_version()
        {
            System.IntPtr ptrVersion = internal_wkhtmltopdf_version();
            return ConstUtf8Marshaler._staticInstance.MarshalNativeToManaged(ptrVersion);
        }


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr wkhtmltopdf_create_global_settings();
        // CAPI(wkhtmltopdf_global_settings *) wkhtmltopdf_create_global_settings();

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_global_settings(System.IntPtr globalSettings);
        // ///////// CAPI(void) wkhtmltopdf_destroy_global_settings(wkhtmltopdf_global_settings *);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr wkhtmltopdf_create_object_settings();
        // CAPI(wkhtmltopdf_object_settings *) wkhtmltopdf_create_object_settings();

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_object_settings(System.IntPtr objectSettings);
        // //////// CAPI(void) wkhtmltopdf_destroy_object_settings(wkhtmltopdf_object_settings *);

        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_set_global_setting"
        , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltopdf_set_global_setting(System.IntPtr settings,
        System.IntPtr name, System.IntPtr value);
        // CAPI(int) wkhtmltopdf_set_global_setting(wkhtmltopdf_global_settings * settings, const char * name, const char * value);

        public static int wkhtmltopdf_set_global_setting(System.IntPtr settings, string name, string value)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltopdf_set_global_setting(settings, ptrName, ptrValue);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        }


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_get_global_setting"
            , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltopdf_get_global_setting(System.IntPtr settings,
            System.IntPtr name, System.IntPtr value, int vs);
        // CAPI(int) wkhtmltopdf_get_global_setting(wkhtmltopdf_global_settings* settings, const char* name, char* value, int vs);


        public static int wkhtmltopdf_get_global_setting(System.IntPtr settings, string name, string value, int vs)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltopdf_get_global_setting(settings, ptrName, ptrValue, vs);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        } // End Function wkhtmltopdf_get_global_setting 


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_set_object_setting"
            , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltopdf_set_object_setting(System.IntPtr objectSettings,
            System.IntPtr name, System.IntPtr value);
        // CAPI(int) wkhtmltopdf_set_object_setting(wkhtmltopdf_object_settings* settings, const char* name, const char* value);

        
        public static int wkhtmltopdf_set_object_setting(System.IntPtr settings, string name, string value)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltopdf_set_object_setting(settings, ptrName, ptrValue);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        }


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_get_object_setting"
            , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern int internal_wkhtmltopdf_get_object_setting(System.IntPtr settings,
            System.IntPtr name, System.IntPtr value, int vs);
        // CAPI(int) wkhtmltopdf_get_object_setting(wkhtmltopdf_object_settings* settings, const char* name, char* value, int vs);

        public static int wkhtmltopdf_get_object_setting(System.IntPtr settings, string name, string value, int vs)
        {
            System.IntPtr ptrName = Utf8Marshaler._staticInstance.MarshalManagedToNative(name);
            System.IntPtr ptrValue = Utf8Marshaler._staticInstance.MarshalManagedToNative(value);

            int ret = internal_wkhtmltopdf_get_object_setting(settings, ptrName, ptrValue, vs);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrName);
            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrValue);

            return ret;
        } // End Function wkhtmltopdf_get_object_setting 



        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr wkhtmltopdf_create_converter(System.IntPtr globalSettings);
        // CAPI(wkhtmltopdf_converter *) wkhtmltopdf_create_converter(wkhtmltopdf_global_settings * settings);


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_converter(System.IntPtr converter);
        // CAPI(void) wkhtmltopdf_destroy_converter(wkhtmltopdf_converter* converter);
        
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_warning_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltopdf_str_callback cb);
        //CAPI(void) wkhtmltopdf_set_warning_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_str_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_error_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltopdf_str_callback cb);
        //CAPI(void) wkhtmltopdf_set_error_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_str_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_phase_changed_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltopdf_void_callback cb);
        //CAPI(void) wkhtmltopdf_set_phase_changed_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_void_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_progress_changed_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltopdf_int_callback cb);
        //CAPI(void) wkhtmltopdf_set_progress_changed_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_int_callback cb);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_finished_callback(System.IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)] wkhtmltopdf_bool_callback cb);
        //CAPI(void) wkhtmltopdf_set_finished_callback(wkhtmltopdf_converter* converter, wkhtmltopdf_int_callback cb);


        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_convert(System.IntPtr converter);
        // CAPI(int) wkhtmltopdf_convert(wkhtmltopdf_converter* converter);


        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_add_object"
            , CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern void internal_wkhtmltopdf_add_object(System.IntPtr converter
            , System.IntPtr objectSettings, System.IntPtr htmlData);
        // CAPI(void) wkhtmltopdf_add_object(
        //     wkhtmltopdf_converter* converter, wkhtmltopdf_object_settings* setting, const char* data);

        public static void wkhtmltopdf_add_object(System.IntPtr converter,
            System.IntPtr objectSettings, string htmlData)
        {
            System.IntPtr ptrData = Utf8Marshaler._staticInstance.MarshalManagedToNative(htmlData);
            internal_wkhtmltopdf_add_object(converter, objectSettings, ptrData);

            Utf8Marshaler._staticInstance.CleanUpNativeData(ptrData);
        }



        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_current_phase(System.IntPtr converter);
        // CAPI(int) wkhtmltopdf_current_phase(wkhtmltopdf_converter* converter);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_phase_count(System.IntPtr converter);
        // CAPI(int) wkhtmltopdf_phase_count(wkhtmltopdf_converter* converter);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        // NOTE: Using IntPtr as return to avoid runtime from freeing returned string. (pruiz)
        public static extern System.IntPtr wkhtmltopdf_phase_description(System.IntPtr converter, int phase);
        // CAPI(const char*) wkhtmltopdf_phase_description(wkhtmltopdf_converter* converter, int phase);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        // NOTE: Using IntPtr as return to avoid runtime from freeing returned string. (pruiz)
        public static extern System.IntPtr wkhtmltopdf_progress_string(System.IntPtr converter);
        // CAPI(const char*) wkhtmltopdf_progress_string(wkhtmltopdf_converter* converter);

        [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_http_error_code(System.IntPtr converter);
        // CAPI(int) wkhtmltopdf_http_error_code(wkhtmltopdf_converter* converter);

        
        
        
        [DllImport(DLL_NAME, EntryPoint = "wkhtmltopdf_get_output", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static extern System.IntPtr internal_wkhtmltopdf_get_output(System.IntPtr converter, out System.IntPtr data);
        // CAPI(long) wkhtmltopdf_get_output(wkhtmltopdf_converter * converter, const unsigned char **);

        public static byte[] wkhtmltopdf_get_output(System.IntPtr converter)
        {
            System.IntPtr data;
            System.IntPtr len = internal_wkhtmltopdf_get_output(converter, out data);
            
            byte[] output = new byte[len.ToInt64()];
            System.Runtime.InteropServices.Marshal.Copy(data, output, 0, output.Length);
            return output;
        } // End Function wkhtmltopdf_get_output 


    } // End Class 


} // End Namespace 
