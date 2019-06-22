using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Bluetooth;

namespace Sama2D.Linkers
{
    public class APILinker
    {
        public const string ServerBase = "https://naox-api.azurewebsites.net/v1/";

    }

    public class BluetoothLinker
    {
        private BluetoothAdapter adapter;
        private BluetoothDevice device;

        public BluetoothLinker(BluetoothAdapter adapter)
        {
            this.adapter = adapter;

            device = FindDevice();
        }

        public BluetoothDevice FindDevice()
        {
            // TODO: Fix empty list
            List<BluetoothDevice> devices = adapter.BondedDevices.ToList();

            return devices[0];
        }
    }
}