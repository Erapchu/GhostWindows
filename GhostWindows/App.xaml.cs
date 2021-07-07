using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace GhostWindows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window1 = new MainWindow(1);
            window1.Show();
            var window2 = new MainWindow(2);
            window2.Show();
            var window3 = new MainWindow(3);
            window3.Show();

            var source = HwndSource.FromHwnd(new WindowInteropHelper(window2).Handle);
            source.AddHook(new HwndSourceHook(WndProc));

            // Switch CacheMode in the second window
            // Then tap Ctrl + ALt + Del or Win + L

            base.OnStartup(e);
        }

        private int _paintCounter;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 15)
            {
                _paintCounter++;
                if (_paintCounter > 1)
                {
                    _paintCounter = 0;
                    // Schedule a redraw, if DWM composition is disabled, DefWindowProc(...WM_NCACTIVATE..) *does* some extra drawing.
                    //var redrawResult = RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate);
                    // Must call DefWindowProc with lParam set to -1, else windows might do some custom drawing in the NC area.

                    //var window = Windows.OfType<Window>().FirstOrDefault(w => new WindowInteropHelper(w).Handle == hwnd);
                    //Current.MainWindow = window;

                    //var defResult = DefWindowProc(hwnd, msg, wParam, new IntPtr(-1));

                    //return defResult;
                }
            }
            else
            {
                _paintCounter = 0;
            }

            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public enum RedrawWindowFlags : uint
        {
            /// <summary>
            /// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
            /// </summary>
            Invalidate = 0x1,

            /// <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.</summary>
            InternalPaint = 0x2,

            /// <summary>
            /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
            /// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
            /// </summary>
            Erase = 0x4,

            /// <summary>
            /// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
            /// This value does not affect internal WM_PAINT messages.
            /// </summary>
            Validate = 0x8,

            NoInternalPaint = 0x10,

            /// <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
            NoErase = 0x20,

            /// <summary>Excludes child windows, if any, from the repainting operation.</summary>
            NoChildren = 0x40,

            /// <summary>Includes child windows, if any, in the repainting operation.</summary>
            AllChildren = 0x80,

            /// <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.</summary>
            UpdateNow = 0x100,

            /// <summary>
            /// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
            /// The affected windows receive WM_PAINT messages at the ordinary time.
            /// </summary>
            EraseNow = 0x200,

            Frame = 0x400,

            NoFrame = 0x800
        }
    }
}
