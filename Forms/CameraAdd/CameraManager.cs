using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraList
{
    internal class CameraManager
    {
        private static List<ICamera> _cameras = new List<ICamera>();

        public static void Add(ICamera camera)
        {
            _cameras.Add(camera); 
        }

        public static List<ICamera> GetAllcameras()
        {
            return _cameras;
        }
    }
}
