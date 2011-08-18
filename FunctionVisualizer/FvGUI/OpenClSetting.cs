using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cloo;

namespace FvGUI
{
    public partial class OpenClSetting : Form
    {
        public OpenClSetting()
        {
            InitializeComponent();
        }

        private void OpenClSetting_Load(object sender, EventArgs e)
        {
            Gpu = null;

            var platforms = ComputePlatform.Platforms;

            if (platforms.Count == 0)
            {
                platformComboBox.Enabled = false;
                deviceComboBox.Enabled = false;
                fpTypeComboBox.Enabled = false;
                okButton.Enabled = false;

                messageLabel.Text = "There's no OpenCL platform installed on this computer";

                return;
            }

            platformComboBox.Items.AddRange(platforms.ToArray());
            platformComboBox.SelectedIndex = 0;
        }

        private void OpenClSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Gpu == null)
            {
                Application.Exit();
            }

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void platformComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (platformComboBox.SelectedIndex < 0)
            {
                deviceComboBox.Enabled = false;
                deviceComboBox.Items.Clear();

                fpTypeComboBox.Enabled = false;
                fpTypeComboBox.Items.Clear();

                okButton.Enabled = false;
                vendorNameLabel.Text = "(unknown)";
                computeUnitLabel.Text = "(unknown)";
                deviceTypeLabel.Text = "(unknown)";

                return;
            }

            var platform = platformComboBox.SelectedItem as ComputePlatform;

            vendorNameLabel.Text = platform.Vendor;

            var devices = platform.Devices;

            if (devices.Count == 0)
            {
                deviceComboBox.Enabled = false;
                deviceComboBox.Items.Clear();

                fpTypeComboBox.Enabled = false;
                fpTypeComboBox.Items.Clear();

                okButton.Enabled = false;
                vendorNameLabel.Text = "(unknown)";
                computeUnitLabel.Text = "(unknown)";
                deviceTypeLabel.Text = "(unknown)";

                messageLabel.Text = "There's no compute device available on this platform.";

                return;
            }

            deviceComboBox.Items.AddRange(devices.ToArray());
            deviceComboBox.SelectedIndex = 0;
        }

        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceComboBox.SelectedIndex < 0)
            {
                fpTypeComboBox.Enabled = false;
                fpTypeComboBox.Items.Clear();

                okButton.Enabled = false;
                computeUnitLabel.Text = "(unknown)";
                deviceTypeLabel.Text = "(unknown)";

                return;
            }

            var device = deviceComboBox.SelectedItem as ComputeDevice;

            computeUnitLabel.Text = device.MaxComputeUnits.ToString();
            deviceTypeLabel.Text = device.Type.ToString();

            //query float point capability

            fpTypeComboBox.Items.Clear();

            fpTypeComboBox.Items.Add("Single Precision");

            fpTypeComboBox.SelectedIndex = 0;

            if (device.Extensions.Contains("cl_amd_fp64"))
            {
                fpTypeComboBox.Items.Add("Double Precision (AMD)");
                fpTypeComboBox.SelectedItem = "Double Precision (AMD)";
            }

            if (device.Extensions.Contains("cl_khr_fp64"))
            {
                fpTypeComboBox.Items.Add("Double Precision");
                fpTypeComboBox.SelectedItem = "Double Precision";
            }
        }

        private void fpTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fpTypeComboBox.SelectedIndex < 0)
            {
                okButton.Enabled = false;
                return;
            }

            messageLabel.Text = "Ready";

            okButton.Enabled = true;
        }

        internal IGpuHelper Gpu { get; private set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            FPType fpType;

            if (fpTypeComboBox.SelectedItem as string == "Double Precision (AMD)")
            {
                fpType = FPType.FP64AMD;
            }
            else if (fpTypeComboBox.SelectedItem as string == "Double Precision")
            {
                fpType = FPType.FP64;
            }
            else
            {
                fpType = FPType.Single;
            }

            Gpu = GpuHelperFactory.CreateHelper(platformComboBox.SelectedItem as ComputePlatform, deviceComboBox.SelectedItem as ComputeDevice, fpType);

            Close();
        }
    }
}
