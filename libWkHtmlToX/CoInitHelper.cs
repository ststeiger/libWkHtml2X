
namespace libWkHtml2X
{


    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa383751(v=vs.85).aspx
    public class CoInitHelper
    {


        private enum RpcAuthnLevel
        {
            Default = 0,
            None = 1,
            Connect = 2,
            Call = 3,
            Pkt = 4,
            PktIntegrity = 5,
            PktPrivacy = 6
        }

        private enum RpcImpLevel
        {
            Default = 0,
            Anonymous = 1,
            Identify = 2,
            Impersonate = 3,
            Delegate = 4
        }

        private enum EoAuthnCap
        {
            None = 0x00,
            MutualAuth = 0x01,
            StaticCloaking = 0x20,
            DynamicCloaking = 0x40,
            AnyAuthority = 0x80,
            MakeFullSIC = 0x100,
            Default = 0x800,
            SecureRefs = 0x02,
            AccessControl = 0x04,
            AppID = 0x08,
            Dynamic = 0x10,
            RequireFullSIC = 0x200,
            AutoImpersonate = 0x400,
            NoCustomMarshal = 0x2000,
            DisableAAA = 0x1000
        }


        
        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        private static extern long CoInitializeSecurity(System.IntPtr pVoid
            , int cAuthSvc, System.IntPtr asAuthSvc, System.IntPtr pReserved1
            , RpcAuthnLevel level, RpcImpLevel impers, System.IntPtr pAuthList
            , EoAuthnCap dwCapabilities, System.IntPtr pReserved3);


        public static long CoInitializeSecurity()
        {
            long coInit = CoInitializeSecurity(System.IntPtr.Zero, -1, System.IntPtr.Zero,
                System.IntPtr.Zero, RpcAuthnLevel.None,
                RpcImpLevel.Impersonate, System.IntPtr.Zero, EoAuthnCap.None, System.IntPtr.Zero);

            return coInit;
        }

        // Note: PreserveSig=false allows .NET interop to handle processing the returned HRESULT and throw an exception on failure
        private enum COINIT : uint 
        {
            COINIT_MULTITHREADED = 0x0, //Initializes the thread for multi-threaded object concurrency.
            COINIT_APARTMENTTHREADED = 0x2, //Initializes the thread for apartment-threaded object concurrency
            COINIT_DISABLE_OLE1DDE = 0x4, //Disables DDE for OLE1 support
            COINIT_SPEED_OVER_MEMORY = 0x8, //Trade memory for speed
        }

        [System.Runtime.InteropServices.DllImport("ole32.dll", SetLastError = true
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "CoInitializeEx"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern int CoInitializeEx(
            [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Optional]
            System.IntPtr pvReserved,
            [System.Runtime.InteropServices.In]
            COINIT dwCoInit //DWORD
        );

        public static long CoInitialize()
        {
            int ret = CoInitializeEx(System.IntPtr.Zero, COINIT.COINIT_APARTMENTTHREADED);
            //int ret = CoInitializeEx(System.IntPtr.Zero, COINIT.COINIT_MULTITHREADED);
            return ret;
        } // End Function CoInitialize 


        [System.Runtime.InteropServices.DllImport("ole32.dll", SetLastError = true
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "CoUninitialize"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern void internal_CoUninitialize();

        public static void CoUninitialize()
        {
            internal_CoUninitialize();
        } // End Function CoUninitialize 



        [System.Runtime.InteropServices.DllImport("ole32.dll", PreserveSig = false)]
        private static extern long OleInitialize(System.IntPtr pvReserved);

        [System.Runtime.InteropServices.DllImport("ole32.dll", PreserveSig = true)]
        private static extern void OleUninitialize();


        public static long InitOle()
        {
            return OleInitialize(System.IntPtr.Zero);
        } // End Function InitOle 


    } // End Class CoInitHelper


} // End Namespace wkHtmlToXCore 
